using System;

Console.WriteLine("Test input file");

double double1 = -27.36666;
double double2 = -33.36666;

string string1 = "this is a string";

/*
	
	Multiline
	Comment

 */

/**/

bool flag = true;
double result = -1;

if (flag)
{
	/* sum */
	result = double1 + double2;
}
else
{
	result = double1 - double2;
}


for (int i = 0; i < 100; i++)
{
	if (result == -1)
    {
		Console.Write(i);
	}
}

int add(int x, int y)
{
	return x + y;
}

class Demo
{
    public int Value { get; set; }

	public override string ToString()
    {
		return $"Value = {Value}";
    }
}
