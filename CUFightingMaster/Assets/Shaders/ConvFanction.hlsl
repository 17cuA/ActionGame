#ifndef CONVFANCTION_INCLUDED
#define CONVFANCTION_INCLUDED

float3 lerp3(float3 x, float3 y, float s)
{
	float3 re;
	re.x = x.x + s*(y.x - x.x);
	re.y = x.y + s*(y.y - x.y);
	re.z = x.z + s*(y.z - x.z);
	return re;
}

#endif
