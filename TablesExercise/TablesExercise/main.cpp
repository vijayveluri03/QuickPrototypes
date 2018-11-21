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
void PerformTest( fpArithmaticAction arithmaticActionCallback, fpRandomizeAction randomizeActionCallback, char* actionStr, char* signStr, bool secondNumberLessThanFirst = false, bool subdueSecondNumber = false );
int GetRandomNumber(int startNum, int endNum );
int GetInput( char* question );
int ArithmaticActionSum( int a, int b );
int ArithmaticActionSubstraction( int a, int b );
int ArithmaticActionMultiplication( int a, int b );
int ArithmaticActionDivision( int a, int b );
int ArithmaticActionReminder( int a, int b );
int GetRandomNumber ( int a, int b, bool subdue, bool overdue );

class TableUnit
{
public:
    int a;
    int b;
    int mul;
    int add;
    
    public: void Calculate ()
    {
        mul = a*b;
        add = a+b;
    }
    
};

int main(int argc, const char * argv[])
{
    switch (GetInput ("Which exam you would take? 1. Tables 2. Additions 3.Substractions 4. Multiplications 5. divisions 6. reminders: "))
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
            printf("Error. this condition is not handled!");
            break;
    }
    
    
    // insert code here...
    printf("\n\nAll Done\n");
    return 0;
}

void PerformTestOnMathTables()
{
    const int startTable = GetInput ("Enter starting table :");
    const int endTable = GetInput ("Enter ending table : ");
    const int startMultiplierNumber = 2;
    const int endMultiplierNumber = 9;
    const int repeatCount = GetInput ("Enter how many times you want to repeat the questions : ");
    
    
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
    if ( (totalCorrect+totalWrong) > 0 )
        printf("\nTotal time taken = %d mins %d secs. Avg time per question = %d secs", (int)(diffTimeInSecs/60.0f),(int)(diffTimeInSecs%60),(int)(diffTimeInSecs/(totalCorrect+totalWrong)));
}

void PerformTest( fpArithmaticAction arithmaticActionCallback, fpRandomizeAction randomizeActionCallback, char* actionStr, char* signStr, bool secondNumberLessThanFirst, bool subdueSecondNumber )
{
    const int startNumber = GetInput ("Min number :");;
    const int endNumber = GetInput ("Max number :");;
    const int questionCount = GetInput ("Question count :");
    
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
int GetRandomNumber(int startNum, int endNum )
{
    return (int)(rand() % (endNum - startNum) + startNum);
}

int GetInput( char* question )
{
    printf("%s", question);
    int option = 0;
    scanf("%d", &option);
    return option;
}

int ArithmaticActionSum( int a, int b )
{
    return a + b;
}
int ArithmaticActionSubstraction( int a, int b )
{
    return a - b;
}
int ArithmaticActionMultiplication( int a, int b )
{
    return a * b;
}
int ArithmaticActionDivision( int a, int b )
{
    return (int)((float)a / b);
}
int ArithmaticActionReminder( int a, int b )
{
    return a % b;
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
