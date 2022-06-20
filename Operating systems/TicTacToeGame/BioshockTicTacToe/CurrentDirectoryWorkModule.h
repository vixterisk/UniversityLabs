#pragma once
#include <windows.h>
#include <string>
#include <iostream>
#include <regex>
#include <codecvt>
#include <locale>

std::wstring ExePath();
std::wstring DirectoryPath();
std::string StringDirectoryPath();
std::string wstring_to_string(const std::wstring& str);
void eraseAllSubStr(std::string& mainStr, const std::string& toErase);