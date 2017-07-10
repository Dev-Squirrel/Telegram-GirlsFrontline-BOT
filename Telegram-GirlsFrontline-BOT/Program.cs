using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram_GirlsFrontline_BOT
{
    class Program
    {
        // TEXT 봇 토큰 읽어와 등록하기
        private static readonly TelegramBotClient Bot = new TelegramBotClient(System.IO.File.ReadAllText(@"resource\BOT_Token.txt"));

        // TEXT 한줄씩 읽어 배열에 추가
        private static string[] textValue1 = System.IO.File.ReadAllLines(@"resource\T-Doll_Production.txt");
        private static string[] textValue2 = System.IO.File.ReadAllLines(@"resource\Equipment_Production.txt");

        static void Main(string[] args)
        {
            Bot.OnMessage += BotOnMessageReceived;
            Bot.OnMessageEdited += BotOnMessageReceived;
            Bot.OnReceiveError += BotOnReceiveError;

            var me = Bot.GetMeAsync().Result;

            Console.Title = me.Username;

            Bot.StartReceiving();
            Console.ReadLine();
            Bot.StopReceiving();
        }

        private static void BotOnReceiveError(object sender, ReceiveErrorEventArgs receiveErrorEventArgs)
        {
            Debugger.Break();
        }

        private static async void BotOnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;

            if (message == null || message.Type != MessageType.TextMessage) return;

            // "!인형제조" OR "!인형제작" 및 여러 명령어 감지
            if (message.Text.StartsWith("!인형제조 ") | message.Text.StartsWith("!인형제작 ") | message.Text.StartsWith("!인형건조 ")
                | message.Text.StartsWith("!총기제조 ") | message.Text.StartsWith("!총기제작 ") | message.Text.StartsWith("!총기건조 ")
                | message.Text.StartsWith("!인형 ") | message.Text.StartsWith("!인제 ") | message.Text.StartsWith("!인조 ")
                | message.Text.StartsWith("!ㅇㅎ ") | message.Text.StartsWith("!ㅇㅈ ")
                | message.Text.StartsWith("!총기 ") | message.Text.StartsWith("!총제 ") | message.Text.StartsWith("!총조 ")
                | message.Text.StartsWith("!ㅊㄱ ") | message.Text.StartsWith("!ㅊㅈ "))
            {
                string result;

                // 2, 4글자 분류
                if (message.Text.StartsWith("!인형 ") | message.Text.StartsWith("!인제 ") | message.Text.StartsWith("!인조 ")
                | message.Text.StartsWith("!ㅇㅎ ") | message.Text.StartsWith("!ㅇㅈ ")
                | message.Text.StartsWith("!총기 ") | message.Text.StartsWith("!총제 ") | message.Text.StartsWith("!총조 ")
                | message.Text.StartsWith("!ㅊㄱ ") | message.Text.StartsWith("!ㅊㅈ "))
                {
                    result = message.Text.Substring(4).Replace(":", "").Replace(";", "");
                }
                else
                {
                    result = message.Text.Substring(6).Replace(":", "").Replace(";", "");
                }

                try
                {
                    // 3글자면 앞에 "0" 추가
                    if (result.Length == 3)
                    {
                        result = "0" + result;
                    }

                    // 텍스트 파일을 한 번에 한 줄씩 읽기
                    for (int i = 0; i < textValue1.Length; i++)
                    {
                        // 텍스트 파일에서 한 줄씩 읽은 데이터 비교하여 문구 출력
                        if (textValue1[i].Substring(0, textValue1[i].IndexOf("|")) == result)
                        {
                            // 서버 프로그램에 검색 성공시 로그 출력
                            Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                            Console.WriteLine(" - 검색 성공");
                            Console.WriteLine(" - 검색 단어 : " + message.Text);
                            Console.WriteLine();
                            // 문구출력
                            await Bot.SendTextMessageAsync(message.Chat.Id, textValue1[i]);
                            // for 탈출
                            break;
                        }

                        // DB에 없거나 잘못 입력한 경우 경고 출력
                        if (i == textValue1.Length - 1)
                        {
                            // 서버 프로그램에 검색 실패시 로그 출력
                            Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                            Console.WriteLine(" - 검색 실패");
                            Console.WriteLine(" - 검색 단어 : " + message.Text);
                            Console.WriteLine();
                            await Bot.SendTextMessageAsync(message.Chat.Id, "DB에 없는 정보 또는 잘못 입력하셨습니다.");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                    Console.WriteLine("에러 발생");
                    Console.WriteLine();
                    await Bot.SendTextMessageAsync(message.Chat.Id, "에러 발생");
                    throw;
                }
            }

            // "!장비제작" 및 여러 명령어 감지
            else if (message.Text.StartsWith("!장비제조 ") | message.Text.StartsWith("!장비제작 ") | message.Text.StartsWith("!장비건조 ")
                     | message.Text.StartsWith("!장비 ") | message.Text.StartsWith("!장제 ") | message.Text.StartsWith("!장건 ")
                     | message.Text.StartsWith("!ㅈㅂ ") | message.Text.StartsWith("!ㅈㅈ ") | message.Text.StartsWith("!ㅈㄱ "))
            {
                string result;

                // 2, 4글자 분류
                if (message.Text.StartsWith("!장비 ") | message.Text.StartsWith("!장제 ") | message.Text.StartsWith("!장건 ")
                    | message.Text.StartsWith("!ㅈㅂ ") | message.Text.StartsWith("!ㅈㅈ ") | message.Text.StartsWith("!ㅈㄱ "))
                {
                    result = message.Text.Substring(4).Replace(":", "").Replace(";", "");
                }
                else
                {
                    result = message.Text.Substring(6).Replace(":", "").Replace(";", "");
                }

                // 2글자면 앞에 "00" 추가, 3글자는 "0" 추가
                if (result.Length == 2)
                {
                    result = "00" + result;
                }
                else if (result.Length == 3)
                {
                    result = "0" + result;
                }

                try
                {
                    // 텍스트 파일을 한 번에 한 줄씩 읽기
                    for (int i = 0; i < textValue2.Length; i++)
                    {
                        // 텍스트 파일에서 한 줄씩 읽은 데이터 비교하여 문구 출력
                        if (textValue2[i].Substring(0, textValue2[i].IndexOf("|")) == result)
                        {
                            // 서버 프로그램에 검색 성공시 로그 출력
                            Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                            Console.WriteLine(" - 검색 성공");
                            Console.WriteLine(" - 검색 단어 : " + message.Text);
                            Console.WriteLine();
                            // 문구출력
                            await Bot.SendTextMessageAsync(message.Chat.Id, textValue2[i]);
                            // for 탈출
                            break;
                        }

                        // DB에 없거나 잘못 입력한 경우 경고 출력
                        if (i == textValue2.Length - 1)
                        {
                            // 서버 프로그램에 검색 실패시 로그 출력
                            Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                            Console.WriteLine(" - 검색 실패");
                            Console.WriteLine(" - 검색 단어 : " + message.Text);
                            Console.WriteLine();
                            await Bot.SendTextMessageAsync(message.Chat.Id, "DB에 없는 정보 또는 잘못 입력하셨습니다.");
                        }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("*************** " + DateTime.Now.ToString() + " ***************");
                    Console.WriteLine("에러 발생");
                    Console.WriteLine();
                    await Bot.SendTextMessageAsync(message.Chat.Id, "에러 발생");
                    throw;
                }
            }

            // "!도움말" 명령어 감지
            else if (message.Text.StartsWith("!도움말"))
            {
                var help = @"사용법:
!인형제조 !인형제작 !인형건조 !총기제조
!총기제작 !총기건조 !인형 !인제 !인조
!ㅇㅎ !ㅇㅈ !총기 !총제 !총조 !ㅊㄱ !ㅊㅈ

예) !인형제조 0:22
!인형 022
!ㅇㅎ 00:22
!ㅇㅈ 0022

!장비제조 !장비제작 !장비건조 !장비
!장제 !장건 !ㅈㅂ !ㅈㅈ !ㅈㄱ

예) !장비제조 0:05
!장비 005
!장제 00:05
!ㅈㅂ 0005
!ㅈㅈ 05 (한자리 X)";
                await Bot.SendTextMessageAsync(message.Chat.Id, help);
            }

            // "/help" 명령어 감지
            else if (message.Text.StartsWith("/help"))
            {
                var help = @"사용법:
!ㅇㅎ - 원하는 시간을 적어주세요.
예) !인형 0022
결과) 0022|★★|PPK

!ㅈㅂ - 원하는 시간을 적어주세요.
예) !장비 0005
결과) 0005|★★|BM 3-12X40(옵티컬)

!도움말 - 상세한 도움말입니다.
/help   - 도움말입니다.";
                await Bot.SendTextMessageAsync(message.Chat.Id, help);
            }
        }
    }
}