#include <stdio.h>
#include <stdlib.h>
#include <tchar.h>
#include <windows.h>
#include <windowsx.h>
#include <cstdlib>
#include <time.h>
#include <iostream>

#define KEY_SHIFTED     0x8000
#define KEY_TOGGLED     0x0001
#define C_KEY		0x43
#define Q_KEY		0x51

const TCHAR szWinName[] = _T("KindaTicTacToeButNotExactly");
const TCHAR szWinClass[] = _T("Win32SampleApp");
const int gridSize = 5;
bool** grid;
WNDCLASS wincl;
HBRUSH hBrush;
HWND hwnd;

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

void ChangeBackgroundColor()
{
	HBRUSH hNewBrush = CreateSolidBrush(RGB(rand() % 105 + 150, rand() % 105 + 150, rand() % 105 + 150));
	SetClassLong(hwnd, GCL_HBRBACKGROUND, (long)hNewBrush);
	DeleteObject(hBrush);
	hBrush = hNewBrush;
	InvalidateRect(hwnd, NULL, FALSE);
}

void PaintGrid(int windowWidth, int windowHeight, HDC hdcMem)
{
	for (int x = 0; x <= windowWidth; x += windowWidth / gridSize)
	{
		MoveToEx(hdcMem, x, 0, NULL);
		LineTo(hdcMem, x, windowHeight);
	}
	for (int y = 0; y <= windowHeight; y += windowHeight / gridSize)
	{
		MoveToEx(hdcMem, 0, y, NULL);
		LineTo(hdcMem, windowWidth, y);
	}
}

void FillCellsWithEllipses(int cellXLength, int cellYLength, HDC hdcMem)
{
	for (int x = 0; x < gridSize; x++)
		for (int y = 0; y < gridSize; y++)
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
	int windowHeight = GetWindowHeight(), windowWidth = GetWindowWidth();
	HDC hdc = BeginPaint(hwnd, &ps);
	auto hdcMem = CreateCompatibleDC(hdc);
	auto hbmMem = CreateCompatibleBitmap(hdc, windowWidth, windowHeight);
	auto hOld = SelectObject(hdcMem, hbmMem);
	FillRect(hdcMem, &ps.rcPaint, hBrush);
	HPEN hPen = CreatePen(PS_SOLID, 1, RGB(0, 0, 0));
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
	int cellXLength = GetWindowWidth() / gridSize;
	int cellYLength = GetWindowHeight() / gridSize;
	switch (message)
	{
	case WM_LBUTTONUP: // left button click
	{
		int xPos = GET_X_LPARAM(lParam);
		int yPos = GET_Y_LPARAM(lParam);
		InvalidateRect(hwnd, NULL, FALSE);
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
	case WM_PAINT:
	{
		PaintEverything(cellXLength, cellYLength);
		return true;
	}
	case WM_SIZE:
	{
		InvalidateRect(hwnd, NULL, FALSE);
		return true;
	}
	case WM_ERASEBKGND:
		return 1;
	}
	return DefWindowProc(hwnd, message, wParam, lParam);
}

int main(int argc, char** argv)
{
	srand(time(NULL));
	BOOL bMessageOk;
	MSG message;
	wincl = { 0 };
	int nCmdShow = SW_SHOW;
	HINSTANCE hThisInstance = GetModuleHandle(NULL);
	wincl.hInstance = hThisInstance;
	wincl.lpszClassName = szWinClass;
	wincl.lpfnWndProc = WindowProcedure;
	hBrush = CreateSolidBrush(RGB(100, 200, 255));
	wincl.hbrBackground = hBrush;
	if (!RegisterClass(&wincl))
		return 0;
	grid = new bool* [gridSize];
	for (int i = 0; i < gridSize; i++)
		grid[i] = new bool[gridSize];
	for (int i = 0; i < gridSize; i++)
		for (int j = 0; j < gridSize; j++)
			grid[i][j] = false;
	hwnd = CreateWindow(
		szWinClass,
		szWinName,
		WS_OVERLAPPEDWINDOW,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		320,
		240,
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
	for (int i = 0; i < gridSize; i++)
	{
		delete[] grid[i];
	}
	delete[] grid;
	DestroyWindow(hwnd);
	UnregisterClass(szWinClass, hThisInstance);
	DeleteObject(hBrush);
	return 0;
}
