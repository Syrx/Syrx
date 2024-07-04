//  ============================================================================================================================= 
//  author       : david sexton (@sextondjc | sextondjc.com)
//  date         : 2017.09.29 (21:39)
//  modified     : 2017.10.01 (20:40)
//  licence      : This file is subject to the terms and conditions defined in file 'LICENSE.txt', which is part of this source code package.
//  =============================================================================================================================

namespace Syrx.Commanders.Databases.Tests.Integration.Models
{
    public record SingleType<T>(
        T One,
        object Parameters = null);
    public record OneType<T1, TResult>(
        T1 One,
        Func<T1, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record TwoType<T1, T2, TResult>(
        T1 One,
        T2 Two,
        Func<T1, T2, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record ThreeType<T1, T2, T3, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        Func<T1, T2, T3, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record FourType<T1, T2, T3, T4, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        Func<T1, T2, T3, T4, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record FiveType<T1, T2, T3, T4, T5, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        Func<T1, T2, T3, T4, T5, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record SixType<T1, T2, T3, T4, T5, T6, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        Func<T1, T2, T3, T4, T5, T6, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record SevenType<T1, T2, T3, T4, T5, T6, T7, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        Func<T1, T2, T3, T4, T5, T6, T7, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record EightType<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record NineType<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
        T1 One,
        T2 Two,
        T3 Three,
        T4 Four,
        T5 Five,
        T6 Six,
        T7 Seven,
        T8 Eight,
        T9 Nine,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record TenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record ElevenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record TwelveType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record ThirteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record FourteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record FifteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
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
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult> Map,
        object Parameters = null,
        string Method = null);
    public record SixteenType<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
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
        T16 Sixteen,
        Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult> Map,
        object Parameters = null,
        string Method = null);
}