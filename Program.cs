// See https://aka.ms/new-console-template for more information
using System.Runtime.ExceptionServices;

Console.WriteLine("Konwersja dowolnego systemu numerycznego w zakresie od 2 do 16 cyfrowej\n");
while (true)
{
    try
    {
        //Pobranie formatu wejściowego
        Console.Write("Podaj format wejściowy 2-16 lub \'q\' aby wyjść: ");
        string tekst = Console.ReadLine();
        if (tekst == "q")
            break;
        int format1 = ConvertFormat(tekst);

        //Pobranie formatu wyjściowego
        Console.Write("Podaj format wyjściowy 2-16 lub \'q\' aby wyjść: ");
        tekst = Console.ReadLine();
        if (tekst == "q")
            break;
        int format2 = ConvertFormat(tekst);

        //Pobranie liczby i wyświetlenie wyniku konwersji
        Console.Write("Podaj liczbę do konwersji: ");
        tekst = Console.ReadLine();
        Console.WriteLine($"Liczba {tekst} w formacie {format1}-cyfrowym to "
            + $"{ConvertAny2Any(tekst, format1, format2)} w formacie {format2}-cyfrowym");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

//Pobranie formatu liczb
static int ConvertFormat(string format)
{
    //Próba konwersji
    int a;
    if (!int.TryParse(format, out a) || a < 2 || a > 16)
        throw new Exception("Należy podać liczbę całkowitą 2-16");
    return a;
}

//Konwersja dowolnego systemu do innego
static string ConvertAny2Any(string wartoscI1, int formatI1, int formatI2)
{
    //Sprawdzenie warunków formatu wejściowe i wyjściowego
    if (formatI1 < 2 || formatI1 > 16 || formatI2 < 2 || formatI2 > 16)
        throw new Exception("Format wejściowy i wyjściowy powinien być w zakresie [2-16]");

    //Sprawdzenie wprowadzonej liczby
    wartoscI1 = wartoscI1.ToUpper();
    foreach (char c in wartoscI1)
    {
        if (c >= '0' && c <= '0' + formatI1 - 1)
            continue;
        else if (c >= 'A' && c <= 'F')
            continue;
        else
        {
            string wiadomosc = "Niepoprawny format danych wejściowych."
                + "Powinien być w zakresie[0-";
            if (formatI1 <= 10)
                wiadomosc += $"{formatI1 - 1}].";
            else if (formatI1 == 11)
                wiadomosc += "9,A].";
            else
                wiadomosc += $"9,A-{'A' + formatI1 - 10}].";
            throw new Exception(wiadomosc);
        }
    }

    //Konwersja na liczbę
    int liczba = 0;
    for (int i = wartoscI1.Length - 1, j = 1; i >= 0; i--, j *= formatI1)
        if (wartoscI1[i] >= '0' && wartoscI1[i] <= '9')
            liczba += (wartoscI1[i] - '0') * j;
        else
            liczba += (wartoscI1[i] - 'A' + 10) * j;

    //Konwersja liczby na nowy format
    if (liczba == 0)
        return "0";
    string wartoscI2 = "";
    while (liczba > 0)
    {
        int modulo = liczba % formatI2;
        if (modulo <= 9)
            wartoscI2 = (char)(modulo + '0') + wartoscI2;
        else
            wartoscI2 = (char)(modulo + 'A' - 10) + wartoscI2;
        liczba /= formatI2;
    }

    //Zwrócenie nowej liczby
    return wartoscI2;
}