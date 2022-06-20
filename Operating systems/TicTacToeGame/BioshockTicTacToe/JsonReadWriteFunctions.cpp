#include"pch.h"
#include"JsonReadWriteFunctions.h"
#include"CurrentDirectoryWorkModule.h"

std::string defaultPNG = StringDirectoryPath() + "\\resources\\bird.png";
std::string defaultJPG = StringDirectoryPath() + "\\resources\\cage.jpg";

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

windowInfo ReadFileViaFILE(const char* filename)
{
	FILE *file;
	windowInfo result;
	errno_t err;
	const size_t BUFFER_SIZE = 1000;
	unsigned char buffer[BUFFER_SIZE];
	if ((err = fopen_s(&file, filename, "r")) != 0)
		result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, defaultPNG, defaultJPG };
	else
	{
		std::string readFile = "";
		fread((void*)buffer, sizeof(buffer[0]), BUFFER_SIZE, file);
		for (int j = 0; j < BUFFER_SIZE && buffer[j] != '}'; j++)
			readFile += buffer[j];
		result = stringParser(readFile);
		fclose(file);
	}
	return result;
}

void WriteFileViaFILE(const char* filename, windowInfo windowInfoCluster)
{
	FILE* file;
	errno_t err;
	const size_t BUFFER_SIZE = 1000;
	char buffer[BUFFER_SIZE];
	for (int i = 0; i < BUFFER_SIZE; i++)
		buffer[i] = '\0';
	snprintf(buffer, sizeof(buffer), "{\n\"gridSize\": \"%d\",\n\"windowWidth\": \"%d\",\n\"windowHeight\": \"%d\",\n\"BackgroundColorRed\": \"%d\",\n\"BackgroundColorGreen\": \"%d\",\n\"BackgroundColorBlue\": \"%d\",\n\"GridColorRed\": \"%d\",\n\"GridColorGreen\": \"%d\",\n\"GridColorBlue\": \"%d\",\n\"IconName1\": \"%s\",\n\"IconName2\": \"%s\"\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue, defaultPNG.c_str(), defaultJPG.c_str());
	if ((err = fopen_s(&file, filename, "w+")) == 0) {
		fwrite(buffer, sizeof(buffer[0]), sizeof(buffer), file);
		fclose(file);
	}
}

windowInfo ReadFileViaFstream(const char* filename)
{
	windowInfo result;
	std::string str, tmp;
	std::ifstream file(filename);
	if (!file.is_open())
	{
		return { 5, 320, 240, 100, 200, 255, 0, 0, 0,defaultPNG, defaultJPG };

	}
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

void WriteFileViaFstream(const char* filename, windowInfo windowInfoCluster)
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
		file << "\"IconName1\": \"" << defaultPNG << "\",\n";
		file << "\"IconName2\": \"" << defaultJPG << "\"\n";
		file << "}";
	}
	file.close();
}

windowInfo ReadFileViaWinapi(const char* filename)
{
	windowInfo result;
	HANDLE hFile = CreateFileA(filename, GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
	if (INVALID_HANDLE_VALUE == hFile)
		result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, defaultPNG, defaultJPG };
	else {
		DWORD outSize = 1;
		DWORD FileSize = GetFileSize(hFile, NULL);
		if (FileSize == INVALID_FILE_SIZE)
		{
			CloseHandle(hFile);
			return { 5, 320, 240, 100, 200, 255, 0, 0, 0, defaultPNG, defaultJPG };
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

void WriteFileViaWinapi(const char* filename, windowInfo windowInfoCluster)
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
		const size_t BUFFER_SIZE = 1000;
		char buffer[BUFFER_SIZE];
		for (int i = 0; i < BUFFER_SIZE; i++)
			buffer[i] = '\0';
		snprintf(buffer, sizeof(buffer), "{\r\n\"gridSize\": \"%d\",\r\n\"windowWidth\": \"%d\",\r\n\"windowHeight\": \"%d\",\r\n\"BackgroundColorRed\": \"%d\",\r\n\"BackgroundColorGreen\": \"%d\",\r\n\"BackgroundColorBlue\": \"%d\",\r\n\"GridColorRed\": \"%d\",\r\n\"GridColorGreen\": \"%d\",\r\n\"GridColorBlue\": \"%d\",\r\n\"IconName1\": \"%s\",\r\n\"IconName2\": \"%s\"\r\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue, defaultPNG.c_str(), defaultJPG.c_str());
		auto bResult = WriteFile(hFile, (LPVOID)& buffer, sizeof(buffer), &outSize, NULL);
	}
	CloseHandle(hFile);
}

windowInfo ReadFileViaFileMapping(const char* filename)
{
	windowInfo result = { 5, 320, 240, 100, 200, 255, 0, 0, 0, defaultPNG, defaultJPG };
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

void  WriteFileViaFileMapping(const char* fname, windowInfo windowInfoCluster)
{
	HANDLE hFile = CreateFileA(fname, GENERIC_READ | GENERIC_WRITE, 0, nullptr, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
	if (hFile == INVALID_HANDLE_VALUE)
		return;
	int buffer_size = 1000;
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
	snprintf(buffer, sizeof(buffer), "{\r\n\"gridSize\": \"%d\",\r\n\"windowWidth\": \"%d\",\r\n\"windowHeight\": \"%d\",\r\n\"BackgroundColorRed\": \"%d\",\r\n\"BackgroundColorGreen\": \"%d\",\r\n\"BackgroundColorBlue\": \"%d\",\r\n\"GridColorRed\": \"%d\",\r\n\"GridColorGreen\": \"%d\",\r\n\"GridColorBlue\": \"%d\",\r\n\"IconName1\": \"%s\",\r\n\"IconName2\": \"%s\"\r\n}", windowInfoCluster.gridSize, windowInfoCluster.windowWidth, windowInfoCluster.windowHeight, windowInfoCluster.BackgroundColorRed, windowInfoCluster.BackgroundColorGreen, windowInfoCluster.BackgroundColorBlue, windowInfoCluster.GridColorRed, windowInfoCluster.GridColorGreen, windowInfoCluster.GridColorBlue, defaultPNG.c_str(), defaultJPG.c_str());
	for (int i = 0; buffer[i] != '}'; i++)
		dataPtr[i] = buffer[i];
	UnmapViewOfFile(dataPtr);
	CloseHandle(hMapping);
	CloseHandle(hFile);
}