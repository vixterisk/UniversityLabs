#include "pch.h"
#pragma comment( lib, "Winmm.lib" )
#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
#include <windows.h>
#include <winuser.h>
#include <windowsx.h>
#include <cstdlib>
#include <time.h>
#include <dos.h>
#include"JsonReadWriteFunctions.h"
#include "CurrentDirectoryWorkModule.h"

#define KEY_SHIFTED     0x8000
#define KEY_TOGGLED     0x0001
#define C_KEY		0x43
#define Q_KEY		0x51
#define KEY_1		0x31
#define KEY_2		0x32
#define KEY_3		0x33
#define KEY_4		0x34

const TCHAR szWinName[] = _T("Bring us the girl, and wipe away the debt.");
const TCHAR szWinClass[] = _T("Win32SampleApp");
int* grid;
WNDCLASS wincl;
HBRUSH hBrush;
HWND hwnd;
HMODULE iconsLibraryHandle;
windowInfo windowInfoCluster;
HANDLE BioshockTicTacToeSemaphore;
HANDLE MutexForEverything;
HANDLE DrawingThread;
bool isItFirstCreatedProccess;
bool isWindowClosing;

int GetClientHeight()
{
	RECT rect;
	if (GetClientRect(hwnd, &rect))
		return rect.bottom - rect.top;
	return -1;
}

int GetClientWidth()
{
	RECT rect;
	if (GetClientRect(hwnd, &rect))
		return rect.right - rect.left;
	return -1;
}

int GetWindowHeight()
{
	RECT rect;
	if (GetWindowRect(hwnd, &rect))
		return rect.bottom - rect.top;
	return -1;
}

int GetWindowWidth()
{
	RECT rect;
	if (GetWindowRect(hwnd, &rect))
		return rect.right - rect.left;
	return -1;
}

void RunNotepad(void)
{
	STARTUPINFO sInfo;
	PROCESS_INFORMATION pInfo;
	ZeroMemory(&sInfo, sizeof(STARTUPINFO));
	puts("Starting Notepad...");
	CreateProcess(_T("C:\\Windows\\Notepad.exe"),
		NULL, NULL, NULL, FALSE, 0, NULL, NULL, &sInfo, &pInfo);
	CloseHandle(pInfo.hProcess);
	CloseHandle(pInfo.hThread);
}


void PaintGrid(int windowWidth, int windowHeight, HDC hdcMem)
{
	int cellSizeX = round(double(windowWidth) / windowInfoCluster.gridSize);
	for (int x = cellSizeX; x < windowWidth; x += cellSizeX)
	{
		MoveToEx(hdcMem, x, 0, NULL);
		LineTo(hdcMem, x, windowHeight);
	}
	int cellSizeY = round(double(windowHeight) / windowInfoCluster.gridSize);
	for (int y = cellSizeY; y < windowHeight; y += cellSizeY)
	{
		MoveToEx(hdcMem, 0, y, NULL);
		LineTo(hdcMem, windowWidth, y);
	}
}

void DrawPNG(char* image, int width, int height, int left, int top, int right, int bottom, int cellXLength, int cellYLength, HDC hdcMem)
{
	COLORREF *arr = (COLORREF*)calloc(width*height, sizeof(COLORREF));
	for (int i = 0, j = 0; i < width*height * 4; i += 4, j++)
	{
		arr[j] = RGB(image[i + 2], image[i + 1], image[i], image[i + 3]);
	}
	HBITMAP map = CreateBitmap(width, height, 1, 4 * 8, (char*)arr);
	HDC src = CreateCompatibleDC(hdcMem);
	SelectObject(src, map);
	BitBlt(hdcMem, (left + right - width) / 2, (top + bottom - height) / 2, cellXLength, cellYLength, src, 0, 0, SRCCOPY);
}

void DrawJPG(char* image, int width, int height, int left, int top, int right, int bottom, int cellXLength, int cellYLength, HDC hdcMem)
{
	COLORREF *arr = (COLORREF*)calloc(width*height, sizeof(COLORREF));
	for (int i = 0, j = 0; i < width*height * 3; i += 3, j++)
	{
		arr[j] = RGB(image[i + 2], image[i + 1], image[i], 255);
	}
	HBITMAP map = CreateBitmap(width, height, 1, 4 * 8, (char*)arr);
	HDC src = CreateCompatibleDC(hdcMem);
	SelectObject(src, map);
	BitBlt(hdcMem, (left + right - width) / 2, (top + bottom - height) / 2, cellXLength, cellYLength, src, 0, 0, SRCCOPY);
}

typedef char*(_cdecl* loadImage)(const char*, int*, int*);
typedef void(_cdecl* freeImage)(char*);

void FillCellsWithContent(int cellXLength, int cellYLength, HDC hdcMem)
{
	for (int i = 0; i < windowInfoCluster.gridSize; i++)
		for (int j = 0; j < windowInfoCluster.gridSize; j++)
		{
			int left = j * cellXLength;
			int right = j * cellXLength + cellXLength;
			int top = i * cellYLength;
			int bottom = i * cellYLength + cellYLength;
			int gridValue;
			WaitForSingleObject(MutexForEverything, INFINITE);
			gridValue = grid[i * windowInfoCluster.gridSize + j];
			ReleaseMutex(MutexForEverything);
			if (gridValue == 1) {
				bool picture = false;
				if (iconsLibraryHandle != NULL) {
					auto load_image = (loadImage)GetProcAddress(iconsLibraryHandle, "load_image");
					if (load_image) {
						int width, height;
						char* image = load_image(windowInfoCluster.IconNamePNG.c_str(), &width, &height);
						if (image != NULL) picture = true;
						DrawPNG(image, width, height, left, top, right, bottom, cellXLength, cellYLength, hdcMem);
						auto free_image = (freeImage)GetProcAddress(iconsLibraryHandle, "free_image");
						free_image(image);
					}
				}
				if (!picture) {
					int substrPart = abs(cellXLength - cellYLength) / 2;
					if (cellXLength > cellYLength)
						Ellipse(hdcMem, left + substrPart, top, right - substrPart, bottom);
					else
						Ellipse(hdcMem, left, top + substrPart, right, bottom - substrPart);
				}
			}
			else if (gridValue == 2) {
				bool picture = false;
				if (iconsLibraryHandle != NULL) {
					auto load_image = (loadImage)GetProcAddress(iconsLibraryHandle, "load_image");
					if (load_image) {
						int width, height;
						char* image = load_image(windowInfoCluster.IconNameJPG.c_str(), &width, &height);
						if (image != NULL) picture = true;
						DrawJPG(image, width, height, left, top, right, bottom, cellXLength, cellYLength, hdcMem);
						auto free_image = (freeImage)GetProcAddress(iconsLibraryHandle, "free_image");
						free_image(image);
					}
				}
				if (!picture) {
					MoveToEx(hdcMem, left, top, NULL);
					LineTo(hdcMem, right, bottom);
					MoveToEx(hdcMem, right, top, NULL);
					LineTo(hdcMem, left, bottom);
				}
			}
		}
}

void PaintEverything(int backgroundColor, int cellXLength, int cellYLength)
{
	int clientWidth = GetClientWidth(), clientHeight = GetClientHeight();
	RECT rc;
	rc.left = 0;
	rc.top = 0;
	rc.right = clientWidth;
	rc.bottom = clientHeight;
	HDC hdc = GetDC(hwnd);
	auto hdcMem = CreateCompatibleDC(hdc);
	auto hbmMem = CreateCompatibleBitmap(hdc, clientWidth, clientHeight);
	auto hOld = SelectObject(hdcMem, hbmMem);
	HBRUSH hNewBrush = CreateSolidBrush(RGB(windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, backgroundColor));
	FillRect(hdcMem, &rc, hNewBrush);
	DeleteObject(hNewBrush);
	HPEN hPen = CreatePen(PS_SOLID, 2, RGB(windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue));
	HGDIOBJ hOldPen = SelectObject(hdcMem, hPen);
	PaintGrid(clientWidth, clientHeight, hdcMem);
	HBRUSH hCircleBrush = CreateSolidBrush(RGB(0, 0, 0));
	HGDIOBJ hOldBrush = SelectObject(hdcMem, hCircleBrush);
	FillCellsWithContent(cellXLength, cellYLength, hdcMem);
	BitBlt(hdc, 0, 0, clientWidth, clientHeight, hdcMem, 0, 0, SRCCOPY);
	SelectObject(hdcMem, hOld);
	DeleteObject(hbmMem);
	DeleteDC(hdcMem);
	SelectObject(hdc, hOldPen);
	DeleteObject(hPen);
	SelectObject(hdc, hOldBrush);
	DeleteObject(hCircleBrush);
	ReleaseDC(hwnd, hdc);
}

DWORD WINAPI Drawing(LPVOID)
{
	double sinArg = 0;
	double sinValue = 0;
	double pi = 3.14159265358979323846;
	while (true) {
		int cellXLength = round(double(GetClientWidth()) / windowInfoCluster.gridSize);
		int cellYLength = round(double(GetClientHeight()) / windowInfoCluster.gridSize);
		sinArg = fmod(sinArg + 0.01, 2 * pi);
		sinValue = sin(sinArg);
		PaintEverything(127 + (int)(100 * sinValue), cellXLength, cellYLength);
		Sleep(10);
		bool stop;
		WaitForSingleObject(MutexForEverything, INFINITE);
		stop = isWindowClosing;
		ReleaseMutex(MutexForEverything);
		if (stop) {
			windowInfoCluster.BackgroundColorRed = 255;
			windowInfoCluster.BackgroundColorGreen = 208;
			windowInfoCluster.BackgroundColorBlue = 160;
			windowInfoCluster.windowWidth = GetWindowWidth();
			windowInfoCluster.windowHeight = GetWindowHeight();
			break;
		}
	}
	CloseHandle(DrawingThread);
	return 0;
}

LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int cellXLength = round(double(GetClientWidth()) / windowInfoCluster.gridSize);
	int cellYLength = round(double(GetClientHeight()) / windowInfoCluster.gridSize);
	switch (message)
	{
		case WM_LBUTTONUP: 
		// left button click
		{
			WaitForSingleObject(MutexForEverything, INFINITE);
			int turn = grid[windowInfoCluster.gridSize*windowInfoCluster.gridSize];
			ReleaseMutex(MutexForEverything);
			if (isItFirstCreatedProccess && !turn || !isItFirstCreatedProccess && turn)
			{
				int xPos = GET_X_LPARAM(lParam);
				int yPos = GET_Y_LPARAM(lParam);
				int xSquare;
				if (xPos / cellXLength >= windowInfoCluster.gridSize) xSquare = windowInfoCluster.gridSize - 1;
				else xSquare = xPos / cellXLength;
				int ySquare = yPos / cellYLength;
				int gridValue;
				WaitForSingleObject(MutexForEverything, INFINITE);
				gridValue = grid[xSquare + windowInfoCluster.gridSize * ySquare];
				ReleaseMutex(MutexForEverything);
				if (gridValue == 0)
				{
					if (isItFirstCreatedProccess) {
						WaitForSingleObject(MutexForEverything, INFINITE);
						grid[xSquare + windowInfoCluster.gridSize * ySquare] = grid[windowInfoCluster.gridSize*windowInfoCluster.gridSize] = 1;
						ReleaseMutex(MutexForEverything);
					}
					else
					{
						WaitForSingleObject(MutexForEverything, INFINITE);
						grid[windowInfoCluster.gridSize*windowInfoCluster.gridSize] = 0;
						grid[xSquare + windowInfoCluster.gridSize * ySquare] = 2;
						ReleaseMutex(MutexForEverything);
					}
				}
			}
			else
				MessageBox(hwnd, _T("It's not your turn."), _T("BioshockTicTacToe"), MB_OK);
			return 0;
		}
		case WM_KEYDOWN: 
		// Key pressed
			switch (LOWORD(wParam))
			{
				case VK_ESCAPE: 
				// Esc
				{
					WaitForSingleObject(MutexForEverything, INFINITE);
					isWindowClosing = true;
					ReleaseMutex(MutexForEverything);
					WaitForSingleObject(DrawingThread, INFINITE);
					PostQuitMessage(0);
					return 0;	
				}			
				case C_KEY: if (GetAsyncKeyState(VK_SHIFT) < 0) RunNotepad(); return true;	
				// Shift + C
				case VK_CONTROL: if (GetAsyncKeyState(Q_KEY) >= 0) return true;			
				// Ctrl + Q
				case KEY_1: SetThreadPriority(DrawingThread, THREAD_PRIORITY_LOWEST); return true; 
				// 1
				case KEY_2: SetThreadPriority(DrawingThread, THREAD_PRIORITY_BELOW_NORMAL); return true; 
				// 2
				case KEY_3: SetThreadPriority(DrawingThread, THREAD_PRIORITY_ABOVE_NORMAL); return true; 
				// 3
				case KEY_4: SetThreadPriority(DrawingThread, THREAD_PRIORITY_HIGHEST); return true; 
				// 4
				case VK_SPACE:
				// spacebar
				{
					int check = SuspendThread(DrawingThread);
					ResumeThread(DrawingThread);
					if (check != 0) ResumeThread(DrawingThread);
					else SuspendThread(DrawingThread); return true;
				}
			}
		case WM_DESTROY: 
		{
			WaitForSingleObject(MutexForEverything, INFINITE);
			isWindowClosing = true;
			ReleaseMutex(MutexForEverything);
			WaitForSingleObject(DrawingThread, INFINITE);
			PostQuitMessage(0);
			return 0;
		}
		case WM_GETMINMAXINFO:
		{
			LPMINMAXINFO lpMMI = (LPMINMAXINFO)lParam;
			lpMMI->ptMinTrackSize.x = 300;
			lpMMI->ptMinTrackSize.y = 320;
			return true;
		}
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}

int main(int argc, char** argv)
{
	BioshockTicTacToeSemaphore = CreateSemaphoreA(NULL, 2, 2, "BioshockTicTacToeSemaphore");
	MutexForEverything = CreateMutexA(NULL, FALSE, "MutexForEverything");
	if (WaitForSingleObject(BioshockTicTacToeSemaphore, 0) == WAIT_TIMEOUT) return 0;
	isWindowClosing = false;
	auto dirPath = DirectoryPath() + L"\\resources\\IconsLibrary.dll";
	iconsLibraryHandle = LoadLibrary(dirPath.c_str());
	char method;
	if (argc > 1)
		method = argv[1][0];
	else method = '1';
	auto filename = StringDirectoryPath() + "\\resources\\input.json";
	const char* nameChar = filename.c_str();
	switch (method)
	{
	case '1': windowInfoCluster = ReadFileViaFILE(nameChar); break;
	case '2': windowInfoCluster = ReadFileViaFstream(nameChar); break;
	case '3': windowInfoCluster = ReadFileViaWinapi(nameChar); break;
	case '4': windowInfoCluster = ReadFileViaFileMapping(nameChar); break;
	}
	// don't forget to free the string after finished using it

	srand(time(NULL));
	BOOL bMessageOk;
	MSG message;
	wincl = { 0 };
	int nCmdShow = SW_SHOW;
	HINSTANCE hThisInstance = GetModuleHandle(NULL);
	wincl.hInstance = hThisInstance;
	wincl.lpszClassName = szWinClass;
	wincl.lpfnWndProc = WindowProcedure;
	hBrush = CreateSolidBrush(RGB(windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue));
	wincl.hbrBackground = hBrush;
	if (!RegisterClass(&wincl))
		return 0;
	int GRIDSIZE = windowInfoCluster.gridSize*windowInfoCluster.gridSize + 1;
	HANDLE checkIfOpen = OpenFileMapping(FILE_MAP_ALL_ACCESS, true, (LPCWSTR)"SharedSpace");
	if (checkIfOpen == NULL)
	{
		dirPath = DirectoryPath() + L"\\resources\\solace.wav";
		PlaySound(dirPath.c_str(), NULL, SND_ASYNC);
		isItFirstCreatedProccess = true;
	}
	else isItFirstCreatedProccess = false;
	CloseHandle(checkIfOpen);
	HANDLE hMapping = CreateFileMapping(nullptr, nullptr, PAGE_READWRITE, 0, GRIDSIZE, (LPCWSTR)"SharedSpace");
	if (hMapping == nullptr) {
		return 0;
	}
	grid = (int*)MapViewOfFile(hMapping, FILE_MAP_ALL_ACCESS, 0, 0, GRIDSIZE);
	if (grid == nullptr) {
		CloseHandle(hMapping);
		return 0;
	}
	if (isItFirstCreatedProccess)
	{
		for (int i = 0; i < windowInfoCluster.gridSize*windowInfoCluster.gridSize; i++)
			grid[i] = 0;
		grid[windowInfoCluster.gridSize*windowInfoCluster.gridSize] = 0;
	}
	auto filenameCursor = StringDirectoryPath() + "\\resources\\bsi_arrow.cur";
	HCURSOR cursor = LoadCursorFromFileA(filenameCursor.c_str());
	hwnd = CreateWindow(
		szWinClass,
		szWinName,
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		windowInfoCluster.windowWidth,
		windowInfoCluster.windowHeight,
		HWND_DESKTOP,
		NULL,
		hThisInstance,
		NULL
	);
	ShowWindow(hwnd, nCmdShow);
	DrawingThread = CreateThread(NULL, 64 * 1024, Drawing, NULL, 0, NULL);
	while ((bMessageOk = GetMessage(&message, NULL, 0, 0)) != 0)
	{
		bool isGameOver = true;
		for (int i = 0; i < windowInfoCluster.gridSize*windowInfoCluster.gridSize; i++)
		{
			int gridValue;
			WaitForSingleObject(MutexForEverything, INFINITE);
			gridValue = grid[i];
			ReleaseMutex(MutexForEverything);
			if (gridValue == 0) isGameOver = false;
		}
		if (isGameOver)
		{
			MessageBox(hwnd, _T("Game is over."), _T("BioshockTicTacToe"), MB_OK);
			WaitForSingleObject(MutexForEverything, INFINITE);
			isWindowClosing = true;
			ReleaseMutex(MutexForEverything);
			WaitForSingleObject(DrawingThread, INFINITE);
			break;
		}
		SetCursor(cursor);
		if (bMessageOk == -1)
		{
			puts("Suddenly, GetMessage failed! You can call GetLastError() to see what happened");
			break;
		}
		TranslateMessage(&message);
		DispatchMessage(&message);
	}
	DestroyWindow(hwnd);
	UnregisterClass(szWinClass, hThisInstance);
	DeleteObject(hBrush);
	UnmapViewOfFile(grid);
	CloseHandle(hMapping);
	FreeLibrary(iconsLibraryHandle);
	switch (method)
	{
	case '1': WriteFileViaFILE(nameChar, windowInfoCluster); break;
	case '2': WriteFileViaFstream(nameChar, windowInfoCluster); break;
	case '3': WriteFileViaWinapi(nameChar, windowInfoCluster); break;
	case '4': WriteFileViaFileMapping(nameChar, windowInfoCluster); break;
	}
	ReleaseSemaphore(BioshockTicTacToeSemaphore, 1, NULL);
	return 0;
}
