using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static byte[,] background = new byte[22, 12]
        {
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},

        };

        //static byte[,] block_L = new byte[4, 4]
        //{
        //    {0, 0, 0, 0},
        //    {0, 1, 0, 0},
        //    {0, 1, 1, 1},
        //    {0, 0, 0, 0},
        //};

        static byte[,,] block_L = new byte[4, 4, 4]
        {
          { {0, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 1, 1, 1},
            {0, 0, 0, 0 }},

          { {0, 0, 0, 0},
            {0, 1, 1, 0},
            {0, 1, 0, 0},
            {0, 1, 0, 0 }},

          { {0, 0, 0, 0},
            {1, 1, 1, 0},
            {0, 0, 1, 0},
            {0, 0, 0, 0 }},

          { {0, 0, 1, 0},
            {0, 0, 1, 0},
            {0, 1, 1, 0},
            {0, 0, 0, 0 }},
        };
        static int count = 0;
        static int x = 3, y = 3;
        static int rotate = 1;

        static void Main(string[] args)
        {
            ConsoleKeyInfo key_value;
            String ch;

            make_background();
            while (true)
            {
                if (Console.KeyAvailable)
                {

                    key_value = Console.ReadKey(true);
                    ch = key_value.Key.ToString();

                    if (ch == "A")
                    {
                        if (overlap_check(-1, 0) == 0)
                        {
                            delete_block();
                            x--;
                            make_block();
                        }
                    }
                    else if (ch == "D")
                    {
                        if (overlap_check(1, 0) == 0)
                        {
                            delete_block();
                            x++;
                            make_block();
                        }
                    }
                    else if (ch == "S")
                    {
                        if (overlap_check(0, 1) == 0)
                        {
                            delete_block();
                            y++;
                            make_block();
                        }
                    }

                    else if (ch == "R")
                    {
                        if (overlap_check_rotate() == 0)
                        {
                            delete_block();
                            rotate++;
                            if (rotate == 4) rotate = 0;
                            make_block();
                        }
                    }
                }

                if (count == 100)
                {
                    count = 0;
                    if (overlap_check(0,1) == 0)
                    {
                        delete_block();
                        y++;
                        make_block();
                    }
                   
                    else
                    {
                        insert_block();
                        print_background_value();
                        for (int i = 1; i<21; i++)
                        {
                            line_check(i);
                        }
                        x = 3;
                        y = 3;
                        rotate = 0;
                    }
                }
                count++;
                Thread.Sleep(10);
            }


        }

        static void delete_block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        Console.SetCursorPosition(i + x, j + y);
                        Console.Write("-");
                    }
                }
            }

        }

        static void make_block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        Console.SetCursorPosition(i + x, j + y);
                        Console.Write("*");
                    }

                }
            }
        }
        static void insert_block()
        {
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1)
                    {
                        background[j + y, i + x] = 1;
                    }

                }
            }
        }


        static int overlap_check(int tmp_x, int tmp_y)
        {
            int overlap_count = 0;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[rotate, j, i] == 1 && background[j + y + tmp_y, i + x + tmp_x] == 1)
                    {
                        overlap_count++;
                    }
                }
            }
            return overlap_count;
        }

        static int overlap_check_rotate()
        {
            int overlap_count = 0;
            int tmp_rotate = rotate;
            tmp_rotate++;
            if (tmp_rotate == 4) tmp_rotate = 0;

            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (block_L[tmp_rotate, j, i] == 1 && background[j + y, i + x] == 1)
                    {
                        overlap_count++;
                    }
                }
            }
            return overlap_count;
        }

        static void make_background()
        {
            for (int j = 0; j < 22; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (background[j, i] == 1)
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("*");
                    }
                    else
                    {
                        Console.SetCursorPosition(i, j);
                        Console.Write("-");
                    }
                }
            }
        }

        static void print_background_value()
        {
            for (int j = 0; j < 22; j++)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (background[j, i] == 1)
                    {
                        Console.SetCursorPosition(i+15, j);
                        Console.Write("1");
                    }
                    else
                    {
                        Console.SetCursorPosition(i+15, j);
                        Console.Write("0");
                    }
                }
            }
        }

        static void line_check(int line_num)
        {
            int block_count = 0;
            for ( int i = 0; i < 10; i++)
            {
                if (background[20, i] == 1)
                {
                    block_count++;
                }
            }

            if (block_count == 10)
            {
                for (int j = 20; j > 1; j--)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        background[j, i] = background[j-1, i];
                    }
                }
                
                

                make_background();
                print_background_value();
            }
        }
    }
}