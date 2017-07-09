using System;
using System.Diagnostics;
using Telegram.Bot;
using Telegram.Bot.Args;
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
            if (message.Text.StartsWith("!인형제조 ") | message.Text.StartsWith("!인형제작 ") | message.Text.StartsWith("!인형건조 ") | message.Text.StartsWith("!총기제조 ") | message.Text.StartsWith("!총기제작 ") | message.Text.StartsWith("!총기건조 "))
            {
                // :, ; 문자열 제거 및 읽을 순서 지정
                string result = message.Text.Substring(6).Replace(":", "").Replace(";", "");

                // 3글자면 앞에 "0" 추가
                if (result.Length == 3)
                {
                    result = "0" + result;
                }

                try
                {
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
            else if (message.Text.StartsWith("!장비제조 ") | message.Text.StartsWith("!장비제작 ") | message.Text.StartsWith("!장비건조 "))
            {
                // :, ; 문자열 제거 및 읽을 순서 지정
                string result = message.Text.Substring(6).Replace(":", "").Replace(";", "");

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

            // "/도움말" 명령어 감지
            else if (message.Text.StartsWith("/help"))
            {
                var help = @"사용법:
!인형제조 OR !총기제조 - 원하는 시간을 적어주세요.
예) !인형제조 0:22 | 022 | 00:22 | 0022
결과) 0022|★★|PPK

!장비제조 - 원하는 시간을 적어주세요.
예) !장비제조 0:05 | 005 | 00:05 | 0005 | 05 (한자리 X)
결과) 0005|★★|BM 3-12X40(옵티컬)

/help   - 도움말입니다.";
                await Bot.SendTextMessageAsync(message.Chat.Id, help);
            }
        }
    }
}