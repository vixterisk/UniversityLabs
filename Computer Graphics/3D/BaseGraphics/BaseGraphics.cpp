#include <iostream>
#include <glm/glm.hpp> 
#include <glm/gtc/matrix_transform.hpp>
#include <glm/gtx/transform.hpp>
#include <GL/glew.h>
#include <GLFW/glfw3.h>
#pragma comment(lib, "glfw3.lib")
#pragma comment(lib, "glew32.lib")
#pragma comment(lib, "opengl32.lib")

using namespace std;

GLFWwindow* g_window;
float g_width, g_height;

GLuint g_shaderProgram;
GLint g_uMVP, g_uMV, g_uN, g_time; 
glm::mat4 g_Projection;

class Model
{
public:
	GLuint vbo;
	GLuint ibo;
	GLuint vao;
	GLsizei indexCount;
};

Model g_model;

GLuint createShader(const GLchar* code, GLenum type)
{
	GLuint result = glCreateShader(type);

	glShaderSource(result, 1, &code, NULL);
	glCompileShader(result);

	GLint compiled;
	glGetShaderiv(result, GL_COMPILE_STATUS, &compiled);

	if (!compiled)
	{
		GLint infoLen = 0;
		glGetShaderiv(result, GL_INFO_LOG_LENGTH, &infoLen);
		if (infoLen > 0)
		{
			char* infoLog = (char*)alloca(infoLen);
			glGetShaderInfoLog(result, infoLen, NULL, infoLog);
			cout << "Shader compilation error" << endl << infoLog << endl;
		}
		glDeleteShader(result);
		return 0;
	}

	return result;
}

GLuint createProgram(GLuint vsh, GLuint fsh)
{
	GLuint result = glCreateProgram();

	glAttachShader(result, vsh);
	glAttachShader(result, fsh);

	glLinkProgram(result);

	GLint linked;
	glGetProgramiv(result, GL_LINK_STATUS, &linked);

	if (!linked)
	{
		GLint infoLen = 0;
		glGetProgramiv(result, GL_INFO_LOG_LENGTH, &infoLen);
		if (infoLen > 0)
		{
			char* infoLog = (char*)alloca(infoLen);
			glGetProgramInfoLog(result, infoLen, NULL, infoLog);
			cout << "Shader program linking error" << endl << infoLog << endl;
		}
		glDeleteProgram(result);
		return 0;
	}

	return result;
}

bool createShaderProgram()
{
	g_shaderProgram = 0;

	const GLchar vsh[] =
		"#version 330\n"
		""
		"layout(location = 0) in vec2 a_position;"
		""
		"uniform mat4 u_mvp;"
		"uniform mat4 u_mv;"
		"uniform mat3 u_n;"
		"uniform float u_time;"
		""
		"out vec3 v_color;"
		"out vec3 v_normal;"
		"out vec3 v_pos;"
		"out float v_time;"
		""
		"float f(vec2 a_pos, float k) {return 4*(k-0.5f)*sin( 100*( (a_pos[0]-0.5f)*(a_pos[0]-0.5f)+(a_pos[1]-0.5f)*(a_pos[1]-0.5f) ) );}"
		""
		"void main()"
		"{"
		"	 v_time = u_time;"
		"    v_color = vec3(0.4, 0.8, 0.8);"
		"	 v_normal = normalize(vec3(f(a_position, a_position[0]), 1.0f, f(a_position, a_position[1])));"
		"	 vec4 p0 = vec4(a_position[0], 0.02 * cos( 100 * ((a_position[0] - 0.5) * (a_position[0] - 0.5) + (a_position[1] - 0.5) * (a_position[1] - 0.5)) ), a_position[1], 1.0f);"
		"	 v_pos = vec3(u_mv * p0);"
		"    gl_Position = u_mvp * p0;"
		"}"
		;

	//const GLchar vsh[] =
	//	"#version 330\n"
	//	""
	//	"layout(location = 0) in vec2 a_position;"
	//	""
	//	"uniform mat4 u_mvp;"
	//	"uniform mat4 u_mv;"
	//	"uniform mat3 u_n;"
	//	""
	//	"out vec3 v_color;"
	//	"out vec3 v_normal;"
	//	"out vec3 v_pos;"
	//	"float a = 0.4f;"
	//	"float b = 0.09f;"
	//	"float c = 100f;"
	//	"float f(vec2 p) {return 0.1*1.5*sin(p.x*p.y);}"
	//	"vec3 grad(vec2 p) {return vec3(2*a*p.x*(c*cos(b*(p.x*p.x+p.y*p.y))-b*sin(b*(p.x*p.x+p.y*p.y))), 1.0f,2*a*p.y*(c*cos(b*(p.x*p.x+p.y*p.y))-b*sin(b*(p.x*p.x+p.y*p.y))));}"
	//	""
	//	"void main()"
	//	"{"
	//	"    v_color = vec3(0.5, 1.0, 0.8);"
	//	"	 v_normal = u_n * normalize(grad(a_position));"
	//	"	 vec4 p0 = vec4(a_position[0], f(a_position), a_position[1], 1.0f);"
	//	"	 v_pos = vec3(u_mv * p0);"		
	//	"    gl_Position = u_mvp * p0;"
	//	"}"
	//	;
	const GLchar fsh[] =
		"#version 330\n"
		""
		"in vec3 v_color;"
		"in vec3 v_normal;"
		"in vec3 v_pos;"
		"in float v_time;"
		""
		"layout(location = 0) out vec4 o_color;"
		""
		"void main()"
		"{"
		"	vec3 E = vec3(0, 0, 0);"
		"	vec3 L = vec3(0, 2.0, 0);"
		"	vec3 n = normalize(v_normal);"
		"	vec3 l = normalize(v_pos - L);"
		"	vec3 e = normalize(E - v_pos);"
		"	vec3 h = normalize(-l + e);"
		"	float d = max(dot(n, -l), 0.1);"
		"	float s = pow(max(dot(n, h), 0), 15);"
		"   o_color = vec4(v_color * d + (v_color + vec3(0.2)) * s, 1.0);"
		"   o_color.rgb = pow(o_color.rgb, vec3(1/2.2));"
//		"   o_color = vec4(abs(n), 1.0);"
		"}"
		;

	GLuint vertexShader, fragmentShader;

	vertexShader = createShader(vsh, GL_VERTEX_SHADER);
	fragmentShader = createShader(fsh, GL_FRAGMENT_SHADER);

	g_shaderProgram = createProgram(vertexShader, fragmentShader);

	g_uMVP = glGetUniformLocation(g_shaderProgram, "u_mvp");
	g_uMV = glGetUniformLocation(g_shaderProgram, "u_mv");
	g_uN = glGetUniformLocation(g_shaderProgram, "u_n"); 
	g_time = glGetUniformLocation(g_shaderProgram, "u_time");

	glDeleteShader(vertexShader);
	glDeleteShader(fragmentShader);

	return g_shaderProgram != 0;
}

bool createModel()
{
	int n = 1000;
	float* vertices = new float[n * n * 2];
	//Генерируем массив вершин сетки n x n
	for (int i = 0; i < n; i++)
		for (int j = 0; j < n * 2; j = j + 2)
		{
			int x = i * n * 2 + j;
			int y = i * n * 2 + j + 1;
			vertices[x] = (float)j /2 / n;
			vertices[y] = (float)i / n;
		}
	int* indices = new int[(n) * (n - 1) * 6];
	//Генерируем массив вершин сетки n x n
	for (int j = 0; j / 6 < (n) * (n - 1); j = j + 6)
	{
		if ((j + 1) / 6 % (n) == (n - 1))
			continue;
		int diff = 6 * ((j + 1) / 6 / n);
		indices[j - diff] = (float)j / 6;
		indices[j + 1 - diff] = (float)(j / 6 + 1);
		indices[j + 2 - diff] = (float)(n + j / 6 + 1);
		indices[j + 3 - diff] = (float)(n + j / 6 + 1);
		indices[j + 4 - diff] = (float)(n + j / 6);
		indices[j + 5 - diff] = (float)j / 6;
	}
	glGenVertexArrays(1, &g_model.vao);
	glBindVertexArray(g_model.vao);

	glGenBuffers(1, &g_model.vbo);
	glBindBuffer(GL_ARRAY_BUFFER, g_model.vbo);
	glBufferData(GL_ARRAY_BUFFER, (n * n * 2) * sizeof(GLfloat), vertices, GL_STATIC_DRAW);

	glGenBuffers(1, &g_model.ibo);
	glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, g_model.ibo);
	glBufferData(GL_ELEMENT_ARRAY_BUFFER, ((n - 1) * (n - 1) * 6) * sizeof(GLuint), indices, GL_STATIC_DRAW);

	g_model.indexCount = (n - 1) * (n - 1) * 6;

	glEnableVertexAttribArray(0);
	glVertexAttribPointer(0, 2, GL_FLOAT, GL_FALSE, 2 * sizeof(GLfloat), (const GLvoid*)0);

	delete[]vertices;
	delete[]indices;
	return g_model.vbo != 0 && g_model.ibo != 0 && g_model.vao != 0;
}

bool init()
{
	// Set initial color of color buffer to white.
	glClearColor(1.0f, 1.0f, 1.0f, 1.0f);
	glEnable(GL_DEPTH_TEST);

	return createShaderProgram() && createModel();
}

void reshape(GLFWwindow* window, int width, int height)
{
	g_width = width;
	g_height = height;
	glViewport(0, 0, width, height);
}

void draw()
{
	// Clear color buffer.
	glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

	glUseProgram(g_shaderProgram);
	glBindVertexArray(g_model.vao);
	//glm::mat4 rotateX = glm::rotate(glm::mat4(1.0f), glm::radians(70.0f), glm::vec3(1, 0, 0));
	//glm::mat4 rotateZ = glm::rotate(rotateX, glm::radians(30.0f), glm::vec3(0, 0, 1));
	//glm::mat4 translate = glm::translate(rotateZ, glm::vec3(-0.5f, -0.5f, 0.0f));

	//glm::mat4 scale = glm::scale(glm::mat4(1.0f), glm::vec3(0.9f, 0.9f, 0.9f));
	glm::mat4 translate = glm::translate(glm::mat4(1.0f), glm::vec3(-0.5f, 0.0f, -0.5f));
	glm::mat4 rotateY = glm::rotate(glm::mat4(1.0f), 0.5f*(float)glfwGetTime(), glm::vec3(0, 1, 0));
	//glm::mat4 translate = glm::translate(glm::mat4(1.0f), glm::vec3(-0.5f, 0.0f, -0.5f));
	glm::mat4 Model = rotateY * translate * glm::mat4(1.0f);
	glm::mat4 View = glm::lookAt(
		glm::vec3(1, 1, 1), // Камера находится в мировых координатах (1,1,1)
		glm::vec3(0, 0, 0), // И направлена в начало координат
		glm::vec3(0, 1, 0)  // направление "upwards" мира
	);
	glm::mat4 g_Projection = glm::perspective(glm::radians(45.0f), float(g_width) / float(g_height), 0.1f, 100.0f);

	glm::mat4 mvp = g_Projection * View * Model;
	//glm::mat4 mvp = g_Projection * Model;
	glUniformMatrix4fv(g_uMVP, 1, GL_FALSE, (GLfloat*)&mvp);

	glm::mat4 mv = View * Model;
	//glm::mat4 mv = Model;
	glUniformMatrix4fv(g_uMV, 1, GL_FALSE, (GLfloat*)&mv);
	glUniform1f(g_time, (GLfloat)glfwGetTime());

	glm::mat3 norm = glm::transpose(glm::inverse(glm::mat3(mv)));
	glUniformMatrix3fv(g_uN, 1, GL_FALSE, (GLfloat*)&norm);

	glDrawElements(GL_TRIANGLES, g_model.indexCount, GL_UNSIGNED_INT, NULL);

	//glm::mat4 mvp = projection * view * (TranslationMatrix * RotationMatrix * ScaleMatrix * OriginalVector);
	//const GLfloat mvp[] =
	//{
	//    2.708748f, -0.478188f, -0.360884f, -0.353738f,
	//    0.000000f, 2.208897f, -2.883250f, -0.865760f,
	//    -0.707388f, -0.479366f, -0.361171f, -0.354019f,
	//    0.000000f, 0.000000f, 0.898990f, 4.000000f
	//};
}

void cleanup()
{
	if (g_shaderProgram != 0)
		glDeleteProgram(g_shaderProgram);
	if (g_model.vbo != 0)
		glDeleteBuffers(1, &g_model.vbo);
	if (g_model.ibo != 0)
		glDeleteBuffers(1, &g_model.ibo);
	if (g_model.vao != 0)
		glDeleteVertexArrays(1, &g_model.vao);
}

bool initOpenGL()
{
	// Initialize GLFW functions.
	if (!glfwInit())
	{
		cout << "Failed to initialize GLFW" << endl;
		return false;
	}

	// Request OpenGL 3.3 without obsoleted functions.
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3);
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	// Create window.
	g_window = glfwCreateWindow(800, 600, "OpenGL Test", NULL, NULL);
	g_width = 800; 
	g_height = 600;
	if (g_window == NULL)
	{
		cout << "Failed to open GLFW window" << endl;
		glfwTerminate();
		return false;
	}

	// Initialize OpenGL context with.
	glfwMakeContextCurrent(g_window);

	// Set internal GLEW variable to activate OpenGL core profile.
	glewExperimental = true;

	// Initialize GLEW functions.
	if (glewInit() != GLEW_OK)
	{
		cout << "Failed to initialize GLEW" << endl;
		return false;
	}

	// Ensure we can capture the escape key being pressed.
	glfwSetInputMode(g_window, GLFW_STICKY_KEYS, GL_TRUE);

	// Set callback for framebuffer resizing event.
	glfwSetFramebufferSizeCallback(g_window, reshape);

	return true;
}

void tearDownOpenGL()
{
	// Terminate GLFW.
	glfwTerminate();
}

int main()
{
	// Initialize OpenGL
	if (!initOpenGL())
		return -1;

	// Initialize graphical resources.
	bool isOk = init();

	if (isOk)
	{
		// Main loop until window closed or escape pressed.
		while (glfwGetKey(g_window, GLFW_KEY_ESCAPE) != GLFW_PRESS && glfwWindowShouldClose(g_window) == 0)
		{
			// Draw scene.
			draw();

			// Swap buffers.
			glfwSwapBuffers(g_window);
			// Poll window events.
			glfwPollEvents();
		}
	}

	// Cleanup graphical resources.
	cleanup();

	// Tear down OpenGL.
	tearDownOpenGL();

	return isOk ? 0 : -1;
}


//bool createModel()
//{
//    //int n = 100;
//    //float* vertices = new float[n * n * 2];
//    //int* indices = new int[(n - 1) * (n - 1) * 6];
//    const GLfloat vertices[] =
//    {
//        -1.0, -1.0, 1.0, 1.0, 0.0, 0.0,
//        1.0, -1.0, 1.0, 1.0, 0.0, 0.0,
//        1.0, 1.0, 1.0, 1.0, 0.0, 0.0,
//        -1.0, 1.0, 1.0, 1.0, 0.0, 0.0,
//
//        1.0, -1.0, 1.0, 1.0, 1.0, 0.0,
//        1.0, -1.0, -1.0, 1.0, 1.0, 0.0,
//        1.0, 1.0, -1.0, 1.0, 1.0, 0.0,
//        1.0, 1.0, 1.0, 1.0, 1.0, 0.0,
//
//        1.0, 1.0, 1.0, 1.0, 0.0, 1.0,
//        1.0, 1.0, -1.0, 1.0, 0.0, 1.0,
//        -1.0, 1.0, -1.0, 1.0, 0.0, 1.0,
//        -1.0, 1.0, 1.0, 1.0, 0.0, 1.0,
//
//        -1.0, 1.0, 1.0, 0.0, 1.0, 1.0,
//        -1.0, 1.0, -1.0, 0.0, 1.0, 1.0,
//        -1.0, -1.0, -1.0, 0.0, 1.0, 1.0,
//        -1.0, -1.0, 1.0, 0.0, 1.0, 1.0,
//
//        -1.0, -1.0, 1.0, 0.0, 1.0, 0.0,
//        -1.0, -1.0, -1.0, 0.0, 1.0, 0.0,
//        1.0, -1.0, -1.0, 0.0, 1.0, 0.0,
//        1.0, -1.0, 1.0, 0.0, 1.0, 0.0,
//
//        -1.0, -1.0, -1.0, 0.0, 0.0, 1.0,
//        -1.0, 1.0, -1.0, 0.0, 0.0, 1.0,
//        1.0, 1.0, -1.0, 0.0, 0.0, 1.0,
//        1.0, -1.0, -1.0, 0.0, 0.0, 1.0,
//    };
//
//    const GLuint indices[] =
//    {
//        0, 1, 2, 2, 3, 0,
//        4, 5, 6, 6, 7, 4,
//        8, 9, 10, 10, 11, 8,
//        12, 13, 14, 14, 15, 12,
//        16, 17, 18, 18, 19, 16,
//        20, 21, 22, 22, 23, 20
//    };
//
//    glGenVertexArrays(1, &g_model.vao);
//    glBindVertexArray(g_model.vao);
//
//    glGenBuffers(1, &g_model.vbo);
//    glBindBuffer(GL_ARRAY_BUFFER, g_model.vbo);
//    glBufferData(GL_ARRAY_BUFFER, 24 * 6 * sizeof(GLfloat), vertices, GL_STATIC_DRAW);
//
//    glGenBuffers(1, &g_model.ibo);
//    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, g_model.ibo);
//    glBufferData(GL_ELEMENT_ARRAY_BUFFER, 6 * 6 * sizeof(GLuint), indices, GL_STATIC_DRAW);
//
//    g_model.indexCount = 6 * 6;
//
//    glEnableVertexAttribArray(0);
//    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(GLfloat), (const GLvoid*)0);
//    glEnableVertexAttribArray(1);
//    glVertexAttribPointer(1, 3, GL_FLOAT, GL_FALSE, 6 * sizeof(GLfloat), (const GLvoid*)(3 * sizeof(GLfloat)));
//
//    //delete []vertices;
//    //delete []indices;
//    return g_model.vbo != 0 && g_model.ibo != 0 && g_model.vao != 0;
//}
