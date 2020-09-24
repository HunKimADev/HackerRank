using System;
using System.Collections.Generic;
using System.Linq;

/*
A DNA sequence can be represented as a string consisting of the letters A, C, G and T, which correspond to the types of successive nucleotides in the sequence. Each nucleotide has an impact factor, which is an integer. Nucleotides of types A, C, G and T have impact factors of 1, 2, 3 and 4, respectively. You are going to answer several queries of the form: What is the minimal impact factor of nucleotides contained in a particular part of the given DNA sequence?

The DNA sequence is given as a non-empty string S = S[0]S[1]...S[N-1] consisting of N characters. There are M queries, which are given in non-empty arrays P and Q, each consisting of M integers. The K-th query (0 ≤ K < M) requires you to find the minimal impact factor of nucleotides contained in the DNA sequence between positions P[K] and Q[K] (inclusive).

For example, consider string S = CAGCCTA and arrays P, Q such that:

    P[0] = 2    Q[0] = 4
    P[1] = 5    Q[1] = 5
    P[2] = 0    Q[2] = 6
The answers to these M = 3 queries are as follows:

The part of the DNA between positions 2 and 4 contains nucleotides G and C (twice), whose impact factors are 3 and 2 respectively, so the answer is 2.
The part between positions 5 and 5 contains a single nucleotide T, whose impact factor is 4, so the answer is 4.
The part between positions 0 and 6 (the whole string) contains all nucleotides, in particular nucleotide A whose impact factor is 1, so the answer is 1.
Write a function:

class Solution { public int[] solution(String S, int[] P, int[] Q); }

that, given a non-empty string S consisting of N characters and two non-empty arrays P and Q consisting of M integers, returns an array consisting of M integers specifying the consecutive answers to all queries.

Result array should be returned as an array of integers.

For example, given the string S = CAGCCTA and arrays P, Q such that:

    P[0] = 2    Q[0] = 4
    P[1] = 5    Q[1] = 5
    P[2] = 0    Q[2] = 6
the function should return the values [2, 4, 1], as explained above.

Write an efficient algorithm for the following assumptions:

N is an integer within the range [1..100,000];
M is an integer within the range [1..50,000];
each element of arrays P, Q is an integer within the range [0..N − 1];
P[K] ≤ Q[K], where 0 ≤ K < M;
string S consists only of upper-case English letters A, C, G, T.
*/



/* wrong answer
class Solution {
    public int[] solution(string S, int[] P, int[] Q) {
        // write your code in C# 6.0 with .NET 4.5 (Mono)
        int[] result = new int[P.Length];
        var counters = new Dictionary<int, int[]>();
        var impact = new Dictionary<char,int>(){
            {'A', 1}, {'C', 2}, {'G', 3}, {'T', 4}
        };
        int[] currCounter = new int[4] {0,0,0,0};
        
        for(int i = 0; i < S.Length; i++)
        {
            int currentLetterIndex = impact[S[i]]-1;
            currCounter[currentLetterIndex]++;
            counters.Add(i,currCounter.ToArray());
        }
        
        for(int i = 0; i < P.Length; i++){
            int from = P[i];
            int to = Q[i];
            
            if(counters[to][impact['A']-1] - counters[from][impact['A']-1] > 0){
                result[i] = impact['A'];
            }else if(counters[to][impact['C']-1] - counters[from][impact['C']-1] > 0){
                result[i] = impact['C'];
            }else if(counters[to][impact['G']-1] - counters[from][impact['G']-1] > 0){
                result[i] = impact['G'];
            }else{
                result[i] = impact['T'];
            }
            
        }
        return result;
    }
}
*/


class Solution {
    public int[] solution(string S, int[] P, int[] Q) {

            var counters = new int[S.Length + 1, 4];
            for (int count = 0; count < S.Length; count++)
            {    
                if (count > 0)
                {              
                    for (int index = 0; index < 4; index++)
                    {
                        counters[count + 1, index] += counters[count, index];
                    }
                }
                switch (S[count])
                {
                    case 'A':
                        counters[count + 1, 0]++;
                        break;
                    case 'C':
                        counters[count + 1, 1]++;
                        break;
                    case 'G':
                        counters[count + 1, 2]++;
                        break;
                    case 'T':
                        counters[count + 1, 3]++;
                        break;
                }
            }

            var result = new int[P.Length];
            for (var count = 0; count < P.Length; count++) {
                if(P[count] == Q[count])
                {
                   switch(S[P[count]]) {
                        case 'A':
                            result[count] = 1;
                            break;
                        case 'C':
                            result[count] = 2;
                            break;
                        case 'G':
                            result[count] = 3;
                            break;
                        case 'T':
                            result[count] = 4;
                            break;
                    }
                } else {
                    for(var index = 0; index < 4; index++) {
                        if((counters[Q[count] + 1, index] - counters[P[count], index]) > 0) {
                            result[count] = index + 1;
                            break;
                        }
                    }
                }
            }

            return result;
    }
}