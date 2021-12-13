using System;

public class Crypto
{
    long public1, public2;
    public Crypto(string input)
    {
        var lines = input.Split(Environment.NewLine);
        public1 = long.Parse(lines[0]);
        public2 = long.Parse(lines[1]);
    }

    public long GetEncryptionKey()
    {
        long loops = 0;
        long mod = 20201227;
        long sub = 7;
        long res = 1;
        do
        {
            loops++;
            res = (res * sub) % mod;
        } while (res != public1);

        sub = public2;
        res = 1;
        for (int i = 0; i < loops; i++)
        {
            res = (res * sub) % mod;
        }
        return res;
    }
}
