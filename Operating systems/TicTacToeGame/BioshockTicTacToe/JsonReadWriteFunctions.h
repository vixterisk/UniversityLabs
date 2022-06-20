#include<string>
#include<vector>
#include <iostream>
#include <fstream>
#include <windows.h>

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
	std::string IconNamePNG;
	std::string IconNameJPG;
};

windowInfo ReadFileViaFILE(const char* filename);

void WriteFileViaFILE(const char* filename, windowInfo windowInfoCluster);

windowInfo ReadFileViaFstream(const char* filename);

void WriteFileViaFstream(const char* filename, windowInfo windowInfoCluster);

windowInfo ReadFileViaWinapi(const char* filename);

void WriteFileViaWinapi(const char* filenam, windowInfo windowInfoCluster);

windowInfo ReadFileViaFileMapping(const char* filename);

void  WriteFileViaFileMapping(const char* fname, windowInfo windowInfoCluster);