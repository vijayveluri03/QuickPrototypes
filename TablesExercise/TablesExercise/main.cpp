//
//  main.cpp
//  TablesExercise
//
//  Created by Vijay Veluri on 03/07/18.
//  Copyright © 2018 Vijay Veluri. All rights reserved.
//

#include <iostream>
#include <vector>
#include <iterator>
#include <algorithm>    // std::random_shuffle
#include <time.h>

typedef int (*fpArithmaticAction)( int a, int b );
typedef int (*fpRandomizeAction)( int a, int b, bool subdue, bool overdue  );

void PerformTestOnMathTables();
void PerformTest( fpArithmaticAction arithmaticActionCallback, fpRandomizeAction randomizeActionCallback, char* actionStr, char* signStr, bool                      secondNumberLessThanFirst = false, bool subdueSecondNumber = false );
int GetRandomNumber(int startNum, int endNum );
int GetInput( char* question );
int ArithmaticActionSum( int a, int b );
int ArithmaticActionSubstraction( int a, int b );
int ArithmaticActionMultiplication( int a, int b );
int ArithmaticActionDivision( int a, int b );
int ArithmaticActionReminder( int a, int b );
int GetRandomNumber ( int a, int b, bool subdue, bool overdue );

const int MIN_TABLE_MULTIPLIER = 2;
const int MAX_TABLE_MULTIPLIER = 9;

class TableUnit
{
public:
    int a, b, mul, add;
    public: void Calculate ()
    {
        mul = a * b;
        add = a + b;
    }
};

int main(int argc, const char * argv[])
{
    switch (GetInput ("Choose the topic you want to take test in : 1. Multiplication tables 2. Additions 3.Substractions 4. Multiplications ( bigger numbers ) 5. divisions 6. reminders \n:"))
    {
        case 1:
            PerformTestOnMathTables();
            break;
        case 2:
            PerformTest( &ArithmaticActionSum, &GetRandomNumber, "Addition", "+" );
            break;
        case 3:
            PerformTest( &ArithmaticActionSubstraction, &GetRandomNumber, "Substraction", "-", true, false );
            break;
        case 4:
            PerformTest( &ArithmaticActionMultiplication, &GetRandomNumber, "Multiplication", "*" );
            break;
        case 5:
            PerformTest( &ArithmaticActionDivision, &GetRandomNumber, "Division", "/", false, true );
            break;
        case 6:
            PerformTest( &ArithmaticActionReminder, &GetRandomNumber, "Reminders", "%", false, true );
            break;
        default:
            printf("Error. This option is invalid!");
            break;
    }
    
    // insert code here...
    printf("\n");
    return 0;
}

void PerformTestOnMathTables()
{
    printf("Note: If you want to test yourself from 4th table to 9th table, your starting table would be 4 and ending table would be 9\n");
    const int startTable = GetInput ("Enter starting table:");
    const int endTable = GetInput ("Enter ending table:");
    
    const int startMultiplierNumber = MIN_TABLE_MULTIPLIER;
    const int endMultiplierNumber = MAX_TABLE_MULTIPLIER;
    
    if ( startTable > endTable )
    {
        printf("invalid entry");
        return;           // todo: dontt exit, ask again
    }
    
    printf("Note:Total number of questions you would face for 1 repetition is %d\n", ( endTable - startTable + 1 ) * ( endMultiplierNumber - startMultiplierNumber + 1 ) );
    const int repeatCount = GetInput ("Enter repetition count ( minimum is 1):");
    
    if ( repeatCount < 1 )
    {
        printf("Repetition count should be greater than 1");
        return;           // todo: dontt exit, ask again
    }
    
    std::vector<TableUnit> tableUnits;
    
    for ( int repCount = 0; repCount < repeatCount; repCount++ )
    {
        for ( int tab = startTable; tab <= endTable; tab ++ )
        {
            for ( int mul = startMultiplierNumber; mul <= endMultiplierNumber; mul ++ )
            {
                TableUnit unit;
                unit.a = tab;
                unit.b = mul;
                unit.Calculate();
                tableUnits.push_back(unit);
            }
        }
    }
    
    std::random_shuffle ( tableUnits.begin(), tableUnits.end() );
    
    int totalCorrect = 0;
    int totalWrong = 0;
    std::vector<TableUnit>::iterator it;
    time_t startTime = time(NULL);
    
    printf("\n\nStarting examination from %d to %d tables. Enter -1 to quit in between. Good Luck!\n\n", startTable, endTable);
    
    for(it = tableUnits.begin(); it != tableUnits.end(); ++it)
    {
        int input = 0;
        printf("%d * %d = ", (*it).a, (*it).b );
        scanf("%d", &input);
        
        if ( input == -1 )
            break;
        
        if ( input == (*it).mul)
        {
            totalCorrect++;
            printf("Correct!\n");
        }
        else
        {
            totalWrong++;
            printf("Wrong\n");
        }
        
        if ( ((totalWrong + totalCorrect) % 10) == 0 )
            printf("Questions remaining: %d\n", tableUnits.size() - (totalWrong + totalCorrect) );
        //printf("%d * %d = %d\n", (*it).a, (*it).b, (*it).mul );
    }
    
    
    time_t endTime = time(NULL);
    int diffTimeInSecs = (int)difftime(endTime,startTime);
    
    printf("\n\nExam complete from %d to %d tables!", startTable, endTable);
    printf("\nNumber of questions answered = %d", (totalCorrect + totalWrong));
    printf("\nTotal correct = %d", totalCorrect);
    printf("\nTotal wrong = %d", totalWrong);
    
    if ( (totalCorrect + totalWrong) > 0 )
        printf("\nTotal time taken = %d mins %d secs. Avg time per question = %d secs", (int)(diffTimeInSecs/60.0f),(int)(diffTimeInSecs%60),(int)(diffTimeInSecs/(totalCorrect+totalWrong)));
}

void PerformTest( fpArithmaticAction arithmaticActionCallback, fpRandomizeAction randomizeActionCallback, char* actionStr, char* signStr, bool secondNumberLessThanFirst, bool subdueSecondNumber )
{
    printf("Note: If you want to test yourself between numbers 40 & 90, the start value would be 40 and end value would be 90\n");
    const int startNumber = GetInput ("Starting number:");
    const int endNumber = GetInput ("Ending number:");
    const int questionCount = GetInput ("How many questions would be like to face:");
    
    int totalCorrect = 0;
    int totalWrong = 0;
    time_t startTime = time(NULL);
    
    printf("\n\nStarting %s test between %d to %d numbers. Enter -1 to quit in between. Good Luck!\n\n", actionStr, startNumber, endNumber);
    
    for( int qCount = 0; qCount < questionCount; qCount++ )
    {
        int input = 0;
        int a = GetRandomNumber(startNumber, endNumber);
        int b = 0;
        if ( secondNumberLessThanFirst )
            b = randomizeActionCallback(startNumber, a, subdueSecondNumber, false);
        else
            b = randomizeActionCallback(startNumber, endNumber, subdueSecondNumber, false);
        
        printf("%d %s %d = ", a, signStr, b );
        scanf("%d", &input);
        
        if ( input == -1 )
            break;
        
        if ( input == arithmaticActionCallback(a,b))
        {
            totalCorrect++;
            printf("Correct!\n");
        }
        else
        {
            totalWrong++;
            printf("Wrong\n");
        }
        
        if ( ((totalWrong + totalCorrect) % 10) == 0 )
            printf("Questions remaining: %d\n", questionCount - (totalWrong + totalCorrect) );
    }
    
    
    time_t endTime = time(NULL);
    int diffTimeInSecs = (int)difftime(endTime,startTime);
    
    printf("\n\%s Exam complete betweeen %d & %d Numbers!", actionStr, startNumber, endNumber);
    printf("\nNumber of questions answered = %d", (totalCorrect + totalWrong));
    
    printf("\nTotal correct = %d", totalCorrect);
    printf("\nTotal wrong = %d", totalWrong);
    
    if ( (totalCorrect+totalWrong) > 0 )
        printf("\nTotal time taken = %d mins %d secs. Avg time per question = %d secs", (int)(diffTimeInSecs/60.0f),(int)(diffTimeInSecs%60),(int)(diffTimeInSecs/(totalCorrect+totalWrong)));
}


// Utility methods
int GetRandomNumber(int startNum, int endNum )      { return (int)(rand() % (endNum - startNum) + startNum); }
int ArithmaticActionSum( int a, int b )             { return a + b; }
int ArithmaticActionSubstraction( int a, int b )    { return a - b; }
int ArithmaticActionMultiplication( int a, int b )  { return a * b; }
int ArithmaticActionDivision( int a, int b )        { return (int)((float)a / b); }
int ArithmaticActionReminder( int a, int b )        { return a % b; }

int GetInput( char* question )
{
    printf("%s", question);
    int option = 0;
    scanf("%d", &option);
    return option;
}

int GetRandomNumber ( int a, int b, bool subdue, bool overdue )
{
    int rand = GetRandomNumber(a,b );
    int temp = 0;
    
    if ( ( !subdue && !overdue )
        || (subdue && overdue )
        )
    {
        return rand;
    }
    else if ( subdue )
    {
        for ( int i = 0; i < 2; i ++ )
        {
            temp = GetRandomNumber(a,b );
            if ( temp < rand )
                rand = temp;
        }
    }
    else if ( overdue )
    {
        for ( int i = 0; i < 2; i ++ )
        {
            temp = GetRandomNumber(a,b );
            if ( temp > rand )
                rand = temp;
        }
    }
    return rand;
}
