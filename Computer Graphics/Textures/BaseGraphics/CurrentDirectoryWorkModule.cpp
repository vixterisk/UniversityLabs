#include "CurrentDirectoryWorkModule.h"

std::wstring ExePath() {
    TCHAR buffer[MAX_PATH] = { 0 };
    GetModuleFileName(NULL, buffer, MAX_PATH);
    std::wstring::size_type pos = std::wstring(buffer).find_last_of(L"\\/");
    return std::wstring(buffer).substr(0, pos);
}

std::string StringDirectoryPath() {
    auto exePath_wstring = ExePath(); 
    auto exePath_string = wstring_to_string(exePath_wstring);
    
    //std::string exePath_string(exePath_wstring.begin(), exePath_wstring.end()); 

    eraseAllSubStr(exePath_string, "\\x64\\Debug");
    return exePath_string;
}

std::string wstring_to_string(const std::wstring& wstr)
{
    if (wstr.empty()) return std::string();
    int size_needed = WideCharToMultiByte(CP_ACP, 0, &wstr[0], (int)wstr.size(), NULL, 0, NULL, NULL);
    std::string strTo(size_needed, 0);
    WideCharToMultiByte(CP_ACP, 0, &wstr[0], (int)wstr.size(), &strTo[0], size_needed, NULL, NULL);
    return strTo;
}

void eraseAllSubStr(std::string& mainStr, const std::string& toErase)
{
    size_t pos = std::string::npos;
    // Search for the substring in string in a loop untill nothing is found
    while ((pos = mainStr.find(toErase)) != std::string::npos)
    {
        // If found then erase it from string
        mainStr.erase(pos, toErase.length());
    }
}

