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
#include <string>
#include <fstream>


using namespace std;

typedef int (*fpArithmaticAction)( int a, int b );
typedef int (*fpRandomizeAction)( int a, int b, bool subdue, bool overdue  );

int GetInput( char* question );



int main(int argc, const char * argv[])
{
    vector<string> resultArray;
    string tempLine = "a";
    ifstream inputFile ("example.txt");
    
    if (inputFile.is_open())
    {
        while ( getline (inputFile,tempLine) )
        {
            resultArray.push_back(tempLine);
        }
        inputFile.close();
    }
    else
    {
        printf("file not found");
    }
    
    printf("\n\nYou enterterd:");
    
    string result;
    for( int i = 0; i < resultArray.size(); i++ )
    {
        result = resultArray[i];
        cout << result << "\n";
    }
    
    printf("\n\n");
    
    ofstream outputFile ("example2.txt");
    if (!outputFile.is_open())
    {
        printf("Unable to read from file ");
        return 1;
    }
    
    int i = 0;
    char ch;
    bool isCompleted = false;
    int rowNumber = 0;
    int colNumber = 0;
    int previousNumber = 0;
    bool isThisFirstEntry = true;
    tempLine = "";
    
    outputFile  << "\n\n[";
    
    for( int j = 0; j < resultArray.size() && !isCompleted;j++ )
    {
        result = resultArray[j];
        i = 0;
        
        while (  i < result.length() )
        {
            ch = result.at(i);
            
            switch ( ch )
            {
                case '!':
                    isCompleted = true;
                    break;
                case '$':
                    
                    if ( previousNumber == 0 )
                        previousNumber = 1;
                    
                    rowNumber += previousNumber;
                    
                    previousNumber = 0;
                    colNumber = 0;
                    break;
                case 'b':
                    
                    if ( previousNumber == 0 )
                        previousNumber = 1;
                    colNumber += previousNumber;
                    
                    previousNumber = 0;
                    break;
                case 'o':
                    {
                        if ( previousNumber == 0 )
                            previousNumber = 1;
                        
                        for ( int j = 0; j < previousNumber; j++ )
                        {
                            if ( !isThisFirstEntry )
                            {
                                //printf(",");
                                outputFile << ",";
                            }
                            outputFile  << "{\"x\":" << colNumber <<",\"y\":" << -rowNumber <<",\"alive\":true}\n";
                            isThisFirstEntry = false;

                            colNumber++;
                        }
                        
                        previousNumber = 0;
                    }
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    {
                        int parsedNumber = ch - '0';
                        previousNumber = previousNumber * 10 + parsedNumber;
                    }
                    break;
                default:
                    printf("\n\nERROR while reading index:%d -> %c ", i, ch);
                    break;
            }
            i++;
        }
    }
    outputFile << "]";
    //printf("]");
    
    outputFile.close();
    
    
    // insert code here...
    printf("\n\nAll Done\n");
    return 0;
}

