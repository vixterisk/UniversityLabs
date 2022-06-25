Small c++ application that renders spinning 3D-model of a water surface with an applied textures and uses openGL API to do so. 

Task: create a program that 
- Renders a 3D-object that represents a surface described by a math equation ![equation](https://latex.codecogs.com/png.image?\inline&space;\dpi{110}\bg{white}0.02&space;cos(100&space;((-0.5&space;&plus;&space;x)^2&space;&plus;&space;(-0.5&space;&plus;&space;y)^2))). Matrix describing this object should be generated automatically, not hard-coded, and vertices should be transformed to create needed shape within the shaders;
- Uses The Blinn–Phong reflection model;
- Applies and blends various textures to that surface;

Texture Images used:

<img src="https://i.imgur.com/TZMXOaC.jpg" width="300" height="300"> <img src="https://i.imgur.com/dTSxtj2.png" width="300" height="300">

Program result:

![Window screenshot](https://i.imgur.com/sUbXFzo.png)

![Window screenshot](https://i.imgur.com/ynYhDMu.png)

![Window screenshot](https://i.imgur.com/xcjfR24.png)

![Window screenshot](https://media.giphy.com/media/PUuDrmN3wjoE79SXkO/giphy.gif)

Just surface shaders:

vertex Shader code:
```GLSL		
layout(location = 0) in vec2 a_position;
		
uniform mat4 u_mvp;
uniform mat4 u_mv;
uniform mat3 u_n;
uniform float u_time;
		
out vec3 v_color;
out vec3 v_normal;
out vec3 v_pos;
out float v_time;
		
float f(vec2 a_pos, float k) {return 4*(k-0.5f)*sin( 100*( (a_pos[0]-0.5f)*(a_pos[0]-0.5f)+(a_pos[1]-0.5f)*(a_pos[1]-0.5f) ) );}
		
void main()
{
	v_time = u_time;
	v_color = vec3(0.4, 0.8, 0.8);
	v_normal = normalize(vec3(f(a_position, a_position[0]), 1.0f, f(a_position, a_position[1])));
	vec4 p0 = vec4(a_position[0], 0.02 * cos( 100 * ((a_position[0] - 0.5) * (a_position[0] - 0.5) + (a_position[1] - 0.5) * (a_position[1] - 0.5)) ), a_position[1], 1.0f);
	v_pos = vec3(u_mv * p0);
	gl_Position = u_mvp * p0;
}
```

fragment Shader code:
```GLSL		
in vec3 v_color;
in vec3 v_normal;
in vec3 v_pos;
in float v_time;
		
layout(location = 0) out vec4 o_color;
		
void main()
{
	vec3 E = vec3(0, 0, 0);
	vec3 L = vec3(0, 2.0, 0);
	vec3 n = normalize(v_normal);
	vec3 l = normalize(v_pos - L);
	vec3 e = normalize(E - v_pos);
	vec3 h = normalize(-l + e);
	float d = max(dot(n, -l), 0.1);
	float s = pow(max(dot(n, h), 0), 15);
	o_color = vec4(v_color * d + (v_color + vec3(0.2)) * s, 1.0);
	o_color.rgb = pow(o_color.rgb, vec3(1/2.2));
}

```
Texture-applied surface shaders:

vertex Shader code:
```GLSL		
layout(location = 0) in vec2 a_position;

uniform mat4 u_mvp;
uniform mat4 u_mv;
uniform mat3 u_n;
uniform float u_time;

out vec2 v_texCoord;
out vec3 v_color;
out vec3 v_normal;
out vec3 v_pos;
out float v_time;

float f(vec2 a_pos, float k) {return 4*(k-0.5f)*sin( 100*( (a_pos[0]-0.5f)*(a_pos[0]-0.5f)+(a_pos[1]-0.5f)*(a_pos[1]-0.5f) ) );}

void main()
{
	v_texCoord = a_position;
	v_time = u_time;
	v_color = vec3(0.6, 1.0, 1.0);
	v_normal = u_n * normalize(vec3(f(a_position, a_position[0]), 1.0f, f(a_position, a_position[1])));
	vec4 p0 = vec4(a_position[0], 0.02 * cos( 100 * ((a_position[0] - 0.5) * (a_position[0] - 0.5) + (a_position[1] - 0.5) * (a_position[1] - 0.5)) ), a_position[1], 1.0f);
	v_pos = vec3(u_mv * p0);
	gl_Position = u_mvp * p0;
}
```

fragment Shader code:
```GLSL		
in vec2 v_texCoord;
in vec3 v_color;
in vec3 v_normal;
in vec3 v_pos;
in float v_time;

uniform sampler2D u_map1;
uniform sampler2D u_map2;

layout(location = 0) out vec4 o_color;

void main()
{
	vec3 E = vec3(-0.5, 1.0, 1.0);
	vec3 L = vec3(1.0, 5.0, 1.0);
	vec3 n = normalize(v_normal);
	vec3 l = normalize(v_pos - L);
	vec3 e = normalize(E - v_pos);
	vec3 h = normalize(-l + e);
	float d = max(dot(n, -l), 0.1);
	float s = pow(max(dot(n, h), 0), 30);
	vec4 t1 = texture(u_map1, v_texCoord);
	t1 = pow(t1, vec4(2.2));
	vec4 t2 = texture(u_map2, v_texCoord);
	t2 = pow(t2, vec4(2.2));
	vec4 t_color = mix(t1, t2, 0.5);
	o_color = vec4(vec3(t_color) * d + vec3(0.6, 0.6, 0.6) * s, 1.0);
	o_color.rgb = pow(o_color.rgb, vec3(1/2.2));
	o_color = vec4(abs(n), 1.0);
}

```
