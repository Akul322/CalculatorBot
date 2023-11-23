
namespace CalculatorBot.Services
{
    public class TextFunctions
    {
        public int Len(string s)
        {
            return s.Length;
        }
        public int? Sum(string s)
        {
            int? total = 0;
            int num = 0;
            string word = "";
            List<int> list = new List<int>();

            word = ReadFirstWord(s);
            while (!String.IsNullOrEmpty(word))
            {
                Console.WriteLine("Слово: " + word);
                if (Int32.TryParse(word, out num))
                    list.Add(num);
                else
                    return null;
                if (s.Length >= word.Length)
                    s = s.Substring(word.Length).Trim();
                word = ReadFirstWord(s);
            };

            if (list.Count > 0)
                total = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                total += list[i];
            }
            return total;
        }
        private string ReadFirstWord(string s)
        {
            string word = "";
            if (String.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            if (s.IndexOf(" ") >= 0)
                word = s.Substring(0, s.IndexOf(" ")).Trim();
            else
                word = s.Trim(); // конец строки, нет пробела - берем все
            return word;
        }
    }
}
