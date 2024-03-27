
using System;
using MJGame.MergeMerchant;
using Random = UnityEngine.Random;

public static class STRING
{
    private static string[] MessageGreetings = {
    "Hey! \n You are ...",
    "Hello!",
    "Hi!",
    "Greetings!"
};

    private static string[] MessageStatus ={
    "Full already!",
    "Ok!",
    "Yyeezz!",
    "Just accept!"
};

    private static string[] MessageMove ={
    "Quickly!",
    "Keep going!",
    "Slow down!",
    "Wait for me"
};

    private static string[] MessageOrder ={
    "Anyone there?",
    "Owner \n Hey!",
    "I'm ordering!",
    "Please \n help me!"
};

    private static string[] MessageWait ={
    "Where's mine?",
    "Almost late!",
    "Not yet?",
    "It's taking too long!"
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
            case TypeMessage.order:
                msg = MessageOrder[Random.Range(0, 4)];
                break;
            case TypeMessage.wait:
                msg = MessageWait[Random.Range(0, 4)];
                break;
        }
        return msg;
    }

    private static string[] MessageRemoveOption = { "Can you delete this Option?", "If the empty slot on the table is full, please delete it!", "Do you still need it?", "It can be deleted whenever you want!", "Adding more space!" };
    private static string[] MessageAdMob = { "Watch Video, receive instant rewards!", "Everything is here!", "What are you looking for?", "Gift video!" };

    public static string GetStringRemveOption()
    {
        return MessageRemoveOption[Random.Range(0, MessageRemoveOption.Length)];
    }

    public static string GetStringAdMobOption()
    {
        return MessageAdMob[Random.Range(0, MessageAdMob.Length)];
    }


}
