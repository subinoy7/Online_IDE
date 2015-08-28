using System;
class demo{
    public static int Main(){
        int a,b,c;
        a=Convert.ToInt32(Console.ReadLine());
        b=Convert.ToInt32(Console.ReadLine());
        c=a+b;
        string msg=Console.ReadLine();
        Console.WriteLine("{0}\nThe string is:{1}",c,msg);
        Console.WriteLine("Helo world");
        return 0;
    }
}