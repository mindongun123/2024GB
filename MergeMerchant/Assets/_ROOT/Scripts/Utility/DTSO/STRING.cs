
using System;
using MJGame.MergeMerchant;
using Random = UnityEngine.Random;

public static class STRING
{
    private static string[] MessageGreetings = {
"Hey!",
"Hello!",
"Hi!",
"Xin chào!",
    };

    private static string[] MessageStatus ={
"Đầy rồi!",
"Ok!",
"Yyeezz!",
"Nhận thôi!"
    };

    private static string[] MessageMove ={
"Nhanh nào!",
"Cố lên!",
"Từ từ nào!",
"Chờ tôi với"
    };

    public static string GetString(TypeMessage typeMessage)
    {
        string msg = " ";
        switch (typeMessage)
        {
            case TypeMessage.greeting:
                msg = MessageGreetings[Random.Range(0, 4)];
                break;
            case TypeMessage.status:
                msg = MessageStatus[Random.Range(0, 4)];
                break;

            case TypeMessage.move:
                msg = MessageMove[Random.Range(0, 4)];
                break;
        }
        return msg;
    }

}
