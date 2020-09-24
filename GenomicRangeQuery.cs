using System;
using System.Collections.Generic;
using System.Linq;


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