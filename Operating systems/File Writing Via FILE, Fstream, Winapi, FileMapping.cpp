#include "pch.h"
#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
#include <windows.h>
#include <windowsx.h>
#include <cstdlib>
#include <time.h>
#include <iostream>
#include <string>
#include <fstream>
#include <vector>

#define KEY_SHIFTED     0x8000
#define KEY_TOGGLED     0x0001
#define C_KEY		0x43
#define Q_KEY		0x51

const TCHAR szWinName[] = _T("KindaTicTacToeButNotExactly");
const TCHAR szWinClass[] = _T("Win32SampleApp");
bool** grid;
WNDCLASS wincl;
HBRUSH hBrush;
HWND hwnd;

struct windowInfo {
	int gridSize;
	int windowWidth;
	int windowHeight;
	int BackgroundColorRed;
	int BackgroundColorGreen;
	int BackgroundColorBlue;
	int GridColorRed;
	int GridColorGreen;
	int GridColorBlue;
	std::string IconName1;
	std::string IconName2;
};
windowInfo windowInfoCluster;

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

void GetNumberFromBuffer(int& i, bool isIChanges, std::string buffer, int& number)
{
	int oldI = i;
	while (buffer[i] != '\"')
	{
		number *= 10;
		number += (buffer[i] - '0');
		i++;
	}
	if (!isIChanges) i = oldI;
}

std::string GetWordFromBuffer(int& i, std::string str)
{
	std::string tmp = "";
	while (str[i] != '\"') {
		tmp += str[i];
		i++;
	}
	return tmp;
}

windowInfo stringParser(std::string str)
{
	std::vector<int> numbers;
	int i = 0, whichQuote = 0, nextNeededQuoteNumber = 3, curNumber = 0;
	while (i < str.length() && numbers.size() < 9)
	{
		if (str[i] == '\"') whichQuote++;
		if (whichQuote == nextNeededQuoteNumber) {
			i++;
			curNumber = 0;
			nextNeededQuoteNumber += 4;
			GetNumberFromBuffer(i, true, str, curNumber);
			whichQuote++;
			numbers.push_back(curNumber);
		}
		i++;
	}
	std::vector<std::string> strings;
	std::string iconName;
	while (i < str.length() && strings.size() < 2)
	{
		if (str[i] == '\"') whichQuote++;
		if (whichQuote == nextNeededQuoteNumber) {
			i++;
			iconName = "";
			nextNeededQuoteNumber += 4;
			iconName = GetWordFromBuffer(i, str);
			strings.push_back(iconName);
			whichQuote++;
		}
		i++;
	}
	return { numbers[0], numbers[1], numbers[2], numbers[3], numbers[4], numbers[5], numbers[6], numbers[7], numbers[8], strings[0], strings[1] };
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

void ChangeBackgroundColor()
{
	int red = rand() % 105 + 150;
	int green = rand() % 105 + 150;
	int blue = rand() % 105 + 150;
	HBRUSH hNewBrush = CreateSolidBrush(RGB(red, green, blue));
	SetClassLong(hwnd, GCL_HBRBACKGROUND, (long)hNewBrush);
	DeleteObject(hBrush);
	hBrush = hNewBrush;
	windowInfoCluster.BackgroundColorRed = red;
	windowInfoCluster.BackgroundColorGreen = green;
	windowInfoCluster.BackgroundColorBlue = blue;
	InvalidateRect(hwnd, NULL, FALSE);
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

void FillCellsWithEllipses(int cellXLength, int cellYLength, HDC hdcMem)
{
	for (int x = 0; x < windowInfoCluster.gridSize; x++)
		for (int y = 0; y < windowInfoCluster.gridSize; y++)
		{
			if (grid[x][y]) {
				int left = x * cellXLength;
				int right = x * cellXLength + cellXLength;
				int top = y * cellYLength;
				int bottom = y * cellYLength + cellYLength;
				int substrPart = abs(cellXLength - cellYLength) / 2;
				if (cellXLength > cellYLength)
					Ellipse(hdcMem, left + substrPart, top, right - substrPart, bottom);
				else
					Ellipse(hdcMem, left, top + substrPart, right, bottom - substrPart);
			}
		}
}

void PaintEverything(int cellXLength, int cellYLength)
{
	PAINTSTRUCT ps;
	int windowHeight = GetClientHeight(), windowWidth = GetClientWidth();
	HDC hdc = BeginPaint(hwnd, &ps);
	auto hdcMem = CreateCompatibleDC(hdc);
	auto hbmMem = CreateCompatibleBitmap(hdc, windowWidth, windowHeight);
	auto hOld = SelectObject(hdcMem, hbmMem);
	FillRect(hdcMem, &ps.rcPaint, hBrush);
	HPEN hPen = CreatePen(PS_SOLID, 1, RGB(windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue));
	HGDIOBJ hOldPen = SelectObject(hdcMem, hPen);
	PaintGrid(windowWidth, windowHeight, hdcMem);
	HBRUSH hCircleBrush = CreateSolidBrush(RGB(0, 0, 0));
	HGDIOBJ hOldBrush = SelectObject(hdcMem, hCircleBrush);
	FillCellsWithEllipses(cellXLength, cellYLength, hdcMem);
	BitBlt(hdc, 0, 0, windowWidth, windowHeight, hdcMem, 0, 0, SRCCOPY);
	SelectObject(hdcMem, hOld);
	DeleteObject(hbmMem);
	DeleteDC(hdcMem);
	SelectObject(hdc, hOldPen);
	DeleteObject(hPen);
	SelectObject(hdc, hOldBrush);
	DeleteObject(hCircleBrush);
	EndPaint(hwnd, &ps);
}

LRESULT CALLBACK WindowProcedure(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	int cellXLength = round(double(GetClientWidth()) / windowInfoCluster.gridSize);
	int cellYLength = round(double(GetClientHeight()) / windowInfoCluster.gridSize);
	switch (message)
	{
	case WM_LBUTTONUP: // left button click
	{
		int xPos = GET_X_LPARAM(lParam);
		int yPos = GET_Y_LPARAM(lParam);
		InvalidateRect(hwnd, NULL, FALSE);
		if (xPos / cellXLength >= windowInfoCluster.gridSize)
			grid[windowInfoCluster.gridSize - 1][yPos / cellYLength] = true;
		else
		grid[xPos / cellXLength][yPos / cellYLength] = true;
		PaintEverything(cellXLength, cellYLength);
		return true;
	}
	case WM_KEYDOWN: // Key pressed
		switch (LOWORD(wParam))
		{
		case VK_RETURN: ChangeBackgroundColor(); return true;				// Enter
		case VK_ESCAPE:	PostQuitMessage(0);	return 0;				// Esc
		case C_KEY: if (GetAsyncKeyState(VK_SHIFT) < 0) RunNotepad(); return true;	// Shift + C
		case Q_KEY: if (GetAsyncKeyState(VK_CONTROL) >= 0) return true;			// Ctrl + Q
		}
	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	case WM_PAINT:
	{
		PaintEverything(cellXLength, cellYLength);
		return true;
	}
	case WM_SIZE:
	{
		InvalidateRect(hwnd, NULL, FALSE);
		windowInfoCluster.windowWidth = GetWindowWidth();
		windowInfoCluster.windowHeight = GetWindowHeight();
		return true;
	}
	case WM_GETMINMAXINFO:
	{
		LPMINMAXINFO lpMMI = (LPMINMAXINFO)lParam;
		lpMMI->ptMinTrackSize.x = 100;
		lpMMI->ptMinTrackSize.y = 100;
		return true;
	}
	case WM_ERASEBKGND:
		return 1;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}

windowInfo ReadFileViaFILE(const char* filename)
{
	FILE *file; 
	windowInfo result;
	errno_t err;
	const size_t BUFFER_SIZE = 1000;
	unsigned char buffer[BUFFER_SIZE];
	if ((err = fopen_s(&file, filename, "r")) != 0) 
		result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, "image1.png", "image2.png" };
	else 
	{
		std::string readFile = "";
		fread((void*)buffer, sizeof(buffer[0]), BUFFER_SIZE, file);
		for (int j = 0; j < BUFFER_SIZE && buffer[j] != '}'; j++)
			readFile += buffer[j];
		result = stringParser(readFile);
	}
	fclose(file);
	return result;
}

void WriteFileViaFILE(const char* filename)
{
	FILE* file;
	errno_t err;
	const size_t BUFFER_SIZE = 300;
	char buffer[BUFFER_SIZE];
	for (int i = 0; i < BUFFER_SIZE; i++)
		buffer[i] = '\0';
	snprintf(buffer, sizeof(buffer), "{\n\"gridSize\": \"%d\",\n\"windowWidth\": \"%d\",\n\"windowHeight\": \"%d\",\n\"BackgroundColorRed\": \"%d\",\n\"BackgroundColorGreen\": \"%d\",\n\"BackgroundColorBlue\": \"%d\",\n\"GridColorRed\": \"%d\",\n\"GridColorGreen\": \"%d\",\n\"GridColorBlue\": \"%d\",\n\"IconName1\": \"%s\",\n\"IconName2\": \"%s\"\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue,windowInfoCluster.IconName1.c_str(),windowInfoCluster.IconName2.c_str());
	if ((err = fopen_s(&file, filename, "w")) == 0)
	{
		fwrite(buffer, sizeof(buffer[0]), sizeof(buffer), file);
	}
	fclose(file);
}

windowInfo ReadFileViaFstream(const char* filename)
{
	windowInfo result;
	std::string str, tmp;
	std::ifstream file(filename);
	if (!file.is_open())
		result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, "image1.png", "image2.png" };
	else {
		while (file.peek() != EOF)
		{
			file >> tmp;
			str += tmp;
		}
		result = stringParser(str);
	}
	file.close();
	return result;
}

void WriteFileViaFstream(const char* filename)
{
	std::ofstream file(filename);
	if (file.is_open())
	{
		file << "{" << "\n";
		file << "\"gridSize\": \"" << windowInfoCluster.gridSize << "\",\n";
		file << "\"windowWidth\": \"" << windowInfoCluster.windowWidth << "\",\n";
		file << "\"windowHeight\": \"" << windowInfoCluster.windowHeight << "\",\n";
		file << "\"BackgroundColorRed\": \"" << windowInfoCluster.BackgroundColorRed << "\",\n";
		file << "\"BackgroundColorGreen\": \"" << windowInfoCluster.BackgroundColorGreen << "\",\n";
		file << "\"BackgroundColorBlue\": \"" << windowInfoCluster.BackgroundColorBlue << "\",\n";
		file << "\"GridColorRed\": \"" << windowInfoCluster.GridColorRed << "\",\n";
		file << "\"GridColorGreen\": \"" << windowInfoCluster.GridColorGreen << "\",\n";
		file << "\"GridColorBlue\": \"" << windowInfoCluster.GridColorBlue << "\",\n";
		file << "\"IconName1\": \"" << windowInfoCluster.IconName1 << "\",\n";
		file << "\"IconName2\": \"" << windowInfoCluster.IconName2 << "\"\n";
		file << "}";
	}
	file.close();
}

windowInfo ReadFileViaWinapi(const char* filename)
{
	windowInfo result;
	HANDLE hFile = CreateFileA(filename, GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (INVALID_HANDLE_VALUE == hFile)
		result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, "image1.png", "image2.png" };
	else {
		DWORD outSize = 1;
		DWORD FileSize = GetFileSize(hFile, NULL); 
		if (FileSize == INVALID_FILE_SIZE)
		{
			CloseHandle(hFile);
			return { 5, 320, 240, 100, 200, 255, 0, 0, 0, "image1.png", "image2.png" };
		}
		char FileBuffer;
		std::string str;
		auto bResult = ReadFile(hFile, (LPVOID)& FileBuffer, sizeof(FileBuffer), &outSize, NULL);
		str += FileBuffer;
		while (!(bResult && outSize == 0)) 
		{
			bResult = ReadFile(hFile, (LPVOID)& FileBuffer, sizeof(FileBuffer), &outSize, NULL);
			str += FileBuffer;
		}
		result = stringParser(str);
	}
	CloseHandle(hFile);
	return result;
}

void WriteFileViaWinapi(const char* filename)
{
	HANDLE hFile = CreateFileA(filename, FILE_APPEND_DATA, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
	if (hFile != INVALID_HANDLE_VALUE) {
		DWORD outSize = 1;
		DWORD FileSize = GetFileSize(hFile, NULL); 
		if (FileSize == INVALID_FILE_SIZE)
		{
			CloseHandle(hFile);
			return;
		}
		const size_t BUFFER_SIZE = 300;
		char buffer[BUFFER_SIZE];
		for (int i = 0; i < BUFFER_SIZE; i++)
			buffer[i] = '\0';
		snprintf(buffer, sizeof(buffer), "{\r\n\"gridSize\": \"%d\",\r\n\"windowWidth\": \"%d\",\r\n\"windowHeight\": \"%d\",\r\n\"BackgroundColorRed\": \"%d\",\r\n\"BackgroundColorGreen\": \"%d\",\r\n\"BackgroundColorBlue\": \"%d\",\r\n\"GridColorRed\": \"%d\",\r\n\"GridColorGreen\": \"%d\",\r\n\"GridColorBlue\": \"%d\",\r\n\"IconName1\": \"%s\",\r\n\"IconName2\": \"%s\"\r\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue, windowInfoCluster.IconName1.c_str(), windowInfoCluster.IconName2.c_str());
		auto bResult = WriteFile(hFile, (LPVOID)& buffer, sizeof(buffer), &outSize, NULL);
	}
	CloseHandle(hFile);
}

windowInfo ReadFileViaFileMapping(const char* filename)
{
	windowInfo result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, "image1.png", "image2.png" };
	HANDLE hFile = CreateFileA(filename, GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (INVALID_HANDLE_VALUE == hFile)
		return result;
	DWORD dwFileSize = GetFileSize(hFile, nullptr);
	if (dwFileSize == INVALID_FILE_SIZE)
	{
		CloseHandle(hFile);
		return result;
	}
	HANDLE hMapping = CreateFileMapping(hFile, nullptr, PAGE_READONLY, 0, 0, nullptr);
	if (hMapping == nullptr) {
		CloseHandle(hFile);
		return result;
	}
	unsigned char* dataPtr = (unsigned char*)MapViewOfFile(hMapping, FILE_MAP_READ, 0, 0, dwFileSize);
	if (dataPtr == nullptr) {
		CloseHandle(hMapping);
		CloseHandle(hFile);
		return result;
	}
	char buffer[500]; std::string str = "";
	memcpy(buffer, (LPVOID)dataPtr, dwFileSize);
	for (int i = 0; i < sizeof(buffer) && buffer[i] != '}'; i++)
		str += buffer[i];
	result = stringParser(str);
	UnmapViewOfFile(dataPtr);
	CloseHandle(hMapping);
	CloseHandle(hFile);
	return result;
}

void  WriteFileViaFileMapping(const char* fname)
{
	HANDLE hFile = CreateFileA(fname, GENERIC_READ | GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (hFile == INVALID_HANDLE_VALUE)
		return;
	int buffer_size = 500;
	HANDLE hMapping = CreateFileMapping(hFile, nullptr, PAGE_READWRITE, 0, buffer_size, nullptr);
	if (hMapping == nullptr) {
		CloseHandle(hFile);
		return;
	}
	unsigned char* dataPtr = (unsigned char*)MapViewOfFile(hMapping, FILE_MAP_ALL_ACCESS, 0, 0, buffer_size);
	if (dataPtr == nullptr) {
		CloseHandle(hMapping);
		CloseHandle(hFile);
		return;
	}
	const int BUFFER_SIZE = 500;
	char buffer[BUFFER_SIZE]; 
	snprintf(buffer, sizeof(buffer), "{\r\n\"gridSize\": \"%d\",\r\n\"windowWidth\": \"%d\",\r\n\"windowHeight\": \"%d\",\r\n\"BackgroundColorRed\": \"%d\",\r\n\"BackgroundColorGreen\": \"%d\",\r\n\"BackgroundColorBlue\": \"%d\",\r\n\"GridColorRed\": \"%d\",\r\n\"GridColorGreen\": \"%d\",\r\n\"GridColorBlue\": \"%d\",\r\n\"IconName1\": \"%s\",\r\n\"IconName2\": \"%s\"\r\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue, windowInfoCluster.IconName1.c_str(), windowInfoCluster.IconName2.c_str());
	for (int i = 0; buffer[i]!='}'; i++)
		dataPtr[i] = buffer[i];
	UnmapViewOfFile(dataPtr);
	CloseHandle(hMapping);
	CloseHandle(hFile);
}

int main(int argc, char** argv)
{
	char method;
	if (argc > 1)
		method = argv[1][0];
	else method = '1';
	const char* filename = "D:\\ProgramData\\Projects2\\OhBoyHereWeGo\\Debug\\input.json";
	switch (method)
	{
		case '1': windowInfoCluster = ReadFileViaFILE(filename); break;
		case '2': windowInfoCluster = ReadFileViaFstream(filename); break;
		case '3': windowInfoCluster = ReadFileViaWinapi(filename); break;
		case '4': windowInfoCluster = ReadFileViaFileMapping(filename); break;
	}
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
	grid = new bool* [windowInfoCluster.gridSize];
	for (int i = 0; i < windowInfoCluster.gridSize; i++)
		grid[i] = new bool[windowInfoCluster.gridSize];
	for (int i = 0; i < windowInfoCluster.gridSize; i++)
		for (int j = 0; j < windowInfoCluster.gridSize; j++)
			grid[i][j] = false;
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
	while ((bMessageOk = GetMessage(&message, NULL, 0, 0)) != 0)
	{
		if (bMessageOk == -1)
		{
			puts("Suddenly, GetMessage failed! You can call GetLastError() to see what happened");
			break;
		}
		TranslateMessage(&message);
		DispatchMessage(&message);
	}
	for (int i = 0; i < windowInfoCluster.gridSize; i++)
	{
		delete[] grid[i];
	}
	delete[] grid;
	DestroyWindow(hwnd);
	UnregisterClass(szWinClass, hThisInstance);
	DeleteObject(hBrush);
	switch (method)
	{
	case '1': WriteFileViaFILE(filename); break;
	case '2': WriteFileViaFstream(filename); break;
	case '3': WriteFileViaWinapi(filename); break;
	case '4': WriteFileViaFileMapping(filename); break;
	}
	return 0;
}
