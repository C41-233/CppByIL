#include <TestDLL.Test.h>
#include <iostream>

int main()
{
    il::int32 x = TestDLL::Test::Add(5, 4);
    std::cout << x << std::endl;
}

