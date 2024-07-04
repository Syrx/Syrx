//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Tests.Integration.Models.Immutable
{
    // the name 'ImmutableType' and various permutations  
    // could have better names. 

    public record ImmutableType(int Id, string Name, decimal Value, DateTime Modified);
    public record ImmutableType<T>(T One);
    public record ImmutableOneType<T1, TResult>(T1 One);
    public record ImmutableTwoType<T1, T2, TResult>(
        T1 One,
        T2 Two);
    public record ImmutableThreeType<T1, T2, T3, TResult>(
        T1 One,
        T2 Two,
        T3 Three);
    public record ImmutableFourType<T1, T2, T3, T4, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four);
    public record ImmutableFiveType<T1, T2, T3, T4, T5, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five);
    public record ImmutableSixType<T1, T2, T3, T4, T5, T6, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six);
    public record ImmutableSevenType<T1, T2, T3, T4, T5, T6, T7, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven);
    public record ImmutableEightType<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight);
    public record ImmutableNineType<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine);
    public record ImmutableTenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten);
    public record ImmutableElevenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven);
    public record ImmutableTwelveType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven,
        T12 Twelve);
    public record ImmutableThirteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven,
        T12 Twelve,
        T13 Thirteen);
    public record ImmutableFourteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven,
        T12 Twelve,
        T13 Thirteen,
        T14 Fourteen);
    public record ImmutableFifteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven,
        T12 Twelve,
        T13 Thirteen,
        T14 Fourteen,
        T15 Fifteen);
    public record ImmutableSixteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        T10 Ten,
        T11 Eleven,
        T12 Twelve,
        T13 Thirteen,
        T14 Fourteen,
        T15 Fifteen,
        T16 Sixteen);
}