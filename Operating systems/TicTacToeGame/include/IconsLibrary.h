#pragma once

#ifdef ICONSLIBRARY_EXPORTS
#define ICONSLIBRARY_API __declspec(dllexport)
#else
#define ICONSLIBRARY_API __declspec(dllimport)
#endif

extern "C" ICONSLIBRARY_API char* load_image(
	const char* filename, int* width, int* height);

extern "C" ICONSLIBRARY_API void free_image(
	char* data);