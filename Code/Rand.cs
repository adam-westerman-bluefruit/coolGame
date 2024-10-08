using System;

public static class Rand
{
    private static Random random;
    private static void init()
    {
        random = new Random();
    }

    public static double Value()
    {
        if(random == null)
            init();
    
        return random.NextDouble();
    }

}