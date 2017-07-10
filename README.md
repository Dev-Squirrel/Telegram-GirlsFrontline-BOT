## 어떤 프로그램인가요?
[Telegram Bot API - C# Client](https://github.com/MrRoundRobin/telegram.bot/tree/master/src/Telegram.Bot)를 사용하여 만든 [소녀전선(少女前线)](http://www.girlsfrontline.co.kr/) 텔레그램 봇입니다.

VS2015로 개발 되었고 빌드에는 [NuGet 패키지 매니저](https://www.nuget.org/)가 필요합니다.

## 봇이 할 수 있는 일은 무엇일까요?
>**사용법:**
>
>!ㅇㅎ - 원하는 시간을 적어주세요.<br />
>예) !인형 0022<br />
>결과) 0022|★★|PPK<br />
><br />
>!ㅈㅂ - 원하는 시간을 적어주세요.<br />
>예) !장비 0005<br />
>결과) 0005|★★|BM 3-12X40(옵티컬)<br />
><br />
>!도움말 - 상세한 도움말입니다.<br />
>/help   - 도움말입니다.<br />

실제 가동중인 봇은 밑에 링크로 들어가시면 사용하실 수 있습니다.

https://telegram.me/GirlsFrontline_BOT

## 텔레그램 봇에 사용 될 토큰은 어디서 구하나요?
밑에 링크를 참조하여 발급 받은 토큰을 Telegram-GirlsFrontline-BOT\resource\BOT_Token.txt 안에 넣어주시면 됩니다.

https://core.telegram.org/bots#6-botfather

## 그룹 채팅방에서 봇이 작동하지 않아요!
[@BotFather](https://telegram.me/botfather)에게 "/setprivacy" 라고 말을 걸어 "Disable" 하시면 그룹 채팅방에서 봇이 작동합니다.

>'Enable' - your bot will only receive messages that either start with the '/' symbol or mention the bot by username.<br />
>'Disable' - your bot will receive all messages that people send to groups.<br />
>Current status is: Disable (기본값: ENABLED)<br />

## LICENSE
MIT License

Copyright (c) 2017 Dev-Squirrel