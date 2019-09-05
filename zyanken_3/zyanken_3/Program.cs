using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zyanken_3
{
    class rule
    {
        public int decision = 0;
        public int PlayerCount;
        public int CpuCount;
        public int FightTimes;
    }
    class HandFlagCount
    {
        public int RockCount = 0;
        public int ScissorsCount = 0;
        public int PaperCount = 0;
        int HandFlag = 0;
        public int HandCount(List<int> list)
        {
            foreach (int item in list)
            {
                if (item == 1)
                {
                    RockCount = 1;
                }
                else if (item == 2)
                {
                    ScissorsCount = 1;
                }
                else
                {
                    PaperCount = 1;
                }
            }
            HandFlag = RockCount + ScissorsCount + PaperCount;
            return HandFlag;
        }
    }

    class judge : HandFlagCount
    {
        public int WinningHand;

        public int DecideWonHand(List<int> list)
        {
            HandCount(list);
            if (RockCount == 0)
            {
                WinningHand = 2;
            }
            else if (ScissorsCount == 0)
            {
                WinningHand = 3;
            }
            else if (PaperCount == 0)
            {
                WinningHand = 1;
            }
            return WinningHand;
        }
    }
    class ResultOutput
    {
        public double ZyankenTimes;
        public int WinningCount = 0;
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("おい、俺たちとジャンケンしろよ。\n");
            Console.WriteLine("ジャンケンしてあげますか？");

            while (true)
            {
                Console.WriteLine("ジャンケンする：Yを入力");
                Console.WriteLine("もう帰る：Nを入力");

                string yn = Console.ReadLine();

                if (yn == "y" || yn == "Y" || yn == "yes" || yn == "YES")
                {
                    JankenBattle();
                }
                else if (yn == "n" || yn == "N" || yn == "no" || yn == "NO")
                {
                    Console.WriteLine("\n帰りたいのを無理に引き留めるのは気が引けるから帰っていいよ\n");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("\nYESかNOか聞かれたならYESかNOで答えろよ\n");
                }
            }
        }
        static void JankenBattle()
        {
            rule rule = new rule();
            ResultOutput ResultOutput = new ResultOutput();
            int i;
            Random EnemyHand = new System.Random();
            List<string> DisplayHand = new List<string>();
            List<int> FieldHand = new List<int>();
            List<double> Wincount = new List<double>();
            List<string> Participant = new List<string>();

            while (true)
            {
                try
                {
                    Console.WriteLine("\n仲間の数を入力してください");
                    rule.PlayerCount = int.Parse(Console.ReadLine()); //プレイヤーの数を入力
                    if (rule.PlayerCount < 0)
                    {
                        throw new InvalidOperationException();
                    }
                    Console.WriteLine("\n敵の数を入力してください（2人以上のみ）");
                    rule.CpuCount = int.Parse(Console.ReadLine()); //CPUの数を入力
                    if (rule.CpuCount < 2)
                    {
                        throw new InvalidOperationException();
                    }
                    Console.WriteLine("\n勝負回数を入力してください");
                    rule.FightTimes = int.Parse(Console.ReadLine()); //何回勝負にするか入力
                    if (rule.FightTimes < 1)
                    {
                        throw new InvalidOperationException();
                    }
                    Console.WriteLine("\n\n闇のゲームの始まりだ\n");
                    break;
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException || ex is InvalidOperationException)
                {
                    Console.Write("\n適切な値を入力してください\n");
                    Console.Write("仲間人数の入力に戻ります\n");
                    continue;
                }

            }
        
            for (i = 0; i < rule.PlayerCount; i++)
            {
                string n = ("仲間" + (i + 1));
                Participant.Add(n);
            }

            for (i = 0; i < rule.CpuCount; i++)
            {
                string n = ("敵" + (i + 1));
                Participant.Add(n);
            }

            for (int Times = 0; Times < rule.FightTimes; Times++)
            {
                Console.WriteLine("\n" + (Times + 1) + "回戦");
                ResultOutput.ZyankenTimes++;

                rule.decision = 0;
                while (rule.decision == 0)
                {
                    for (i = 0; i < rule.PlayerCount; i++)
                    {
                        Console.WriteLine("\n仲間" + (i + 1) + "の手を選択し、以下の数字で入力");
                        Console.WriteLine("1:グー　2:チョキ　3:パー");
                        FieldHand.Add(GetInt(ref i));
                    }

                    for (i = 0; i < rule.CpuCount; i++)
                    {
                        FieldHand.Add(EnemyHand.Next(1, 4));
                    }

                    foreach (int item in FieldHand)
                    {
                        if (item == 1)
                        {
                            DisplayHand.Add("グー");
                        }
                        else if (item == 2)
                        {
                            DisplayHand.Add("チョキ");
                        }
                        else if (item == 3)
                        {
                            DisplayHand.Add("パー");
                        }
                    }

                    for (int j = 0; j < rule.PlayerCount; j++)
                    {
                        Console.Write("\n仲間" + (j + 1) + ":" + DisplayHand[j] + " ");
                    }
                    for (int j = rule.PlayerCount; j < rule.PlayerCount + rule.CpuCount; j++)
                    {
                        Console.Write("\n　敵" + (j - rule.PlayerCount + 1) + ":" + DisplayHand[j] + " ");
                    }
                    //DisplayHand.Clear();

                    HandFlagCount HandFlagCount = new HandFlagCount();
                    int x = 0;
                    x = HandFlagCount.HandCount(FieldHand);
                    judge judge = new judge();

                    if (x == 1 || x == 3)
                    {
                        Console.WriteLine("\n\n>>>あいこだぜ!!<<<");
                        Console.WriteLine("あいこが出たのでもう一度↓\n");
                        FieldHand.Clear();
                        continue;
                    }
                    else
                    {
                        int WinningHand = judge.DecideWonHand(FieldHand);

                        Console.WriteLine("\n\n＞＞＞" + DisplayHand[i] + "の勝ちだぜ！！＜＜＜");
                        Console.WriteLine("\n\n勝者\n");
                        for (i = 0; i < rule.PlayerCount + rule.CpuCount; i++)
                        {
                            Wincount.Add(0);

                            if (FieldHand[i] == WinningHand)
                            {
                                Console.WriteLine(Participant[i]);
                                ResultOutput.WinningCount++;
                                Wincount[i] += 1;
                            }
                        }
                        Console.WriteLine("\n--------------------------------------");
                    }
                    FieldHand.Clear();
                    rule.decision = 1;
                }
            }
            rule.decision = 0;

            while (true)
            {
                Console.WriteLine("\n成績を見たいですか？");
                Console.WriteLine("Y:見たい　　N：興味ないね");
                string yn = Console.ReadLine();
                if (yn == "y" || yn == "Y" || yn == "yes" || yn == "YES")
                {
                    Console.WriteLine("「結果発表～～～～～～～～～～～～～～～～～～～～～～～」(cv.浜田雅功)\n");
                    for (i = 0; i < rule.PlayerCount + rule.CpuCount; i++)
                    {
                        float WinRate = ((float)Wincount[i] / (float)ResultOutput.ZyankenTimes) * 100;
                        Console.Write(Participant[i] + ":");
                        Console.WriteLine("勝ち：" + Wincount[i] + "回、" + "負け：" + (ResultOutput.ZyankenTimes - Wincount[i]) + "回、勝率:" + WinRate + "%");
                    }
                    break;
                }
                else if (yn == "n" || yn == "N" || yn == "no" || yn == "NO")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nYESかNOか聞かれたならYESかNOで答えろよ\n"); //入力が適正でない場合入力に戻る
                }
                Console.WriteLine("\n\nもう一度遊びますか？\n");
            }
        }
        static int GetInt(ref int x)
        {
            int i;
            while (true)
            {
                try
                {
                    i = int.Parse(Console.ReadLine());
                    if (i > 3)
                    {
                        throw new InvalidOperationException();
                    }
                    if (i < 1)
                    {
                        throw new InvalidOperationException();
                    }
                }
                catch (Exception ex) when (ex is FormatException || ex is OverflowException || ex is InvalidOperationException)
                {
                    Console.Write("\n正しい値を入力してください\n");
                    Console.WriteLine("1:グー　2:チョキ　3:パー");
                    continue;
                }
                break;
            }
            return i;
        }
    }
}