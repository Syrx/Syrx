using static Syrx.Commanders.Databases.Tests.Integration.Models.ModelGenerators.ImmutableTypeOptionsBuilder;

namespace Syrx.Commanders.Databases.Tests.Integration.Models
{
    public class ModelGenerators
    {
        public class Multimap
        {

            /// <summary>
            /// Most of the types returned are records types as 
            /// we get Equality comparison for free - records 
            /// implement value equality automatically
            /// https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/how-to-define-value-equality-for-a-type
            /// </summary>


            public static IEnumerable<object[]> SingleTypeData => [
                [1, new SingleType<ImmutableType>(new ImmutableType(1, "entry 1", 10, DateTime.Today))],
                [1, new SingleType<MutableType>(new MutableType { Id = 1, Name = "entry 1", Value = 10, Modified = DateTime.Today })],
                [2, new SingleType<RecordType>(new RecordType(2, "entry 2", 20, DateTime.Today))],
                [3, new SingleType<PrimaryConstructorImmutableType>(new PrimaryConstructorImmutableType(3, "entry 3", 30, DateTime.Today))]
               ];
            
            public static IEnumerable<object[]> TwoTypeData => [
                [1, 140, new TwoType<ImmutableType, ImmutableType, ImmutableTwoType<ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                (a, b) => new ImmutableTwoType<ImmutableType, ImmutableType, ImmutableType>(a, b))],

                // this is a more interesting case in that it reflects a more realistic/common domain model. 
                [1, 140, new TwoType<ImmutableType, ImmutableType, ImmutableFiveType<int, string, decimal, DateTime, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                (a, b) => new ImmutableFiveType<int, string, decimal, DateTime, ImmutableType, ImmutableType>(a.Id, a.Name, a.Value, a.Modified, b))]
             ];

            public static IEnumerable<object[]> ThreeTypeData => [
                [1, 130, new ThreeType<ImmutableType, ImmutableType, ImmutableType,
                ImmutableThreeType<ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                (a, b, c) => new ImmutableThreeType<ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c))
             ],
             [1, 130, new ThreeType<ImmutableType, ImmutableType, ImmutableType,
                 ImmutableThreeType<ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                 (a, b, c) => new ImmutableThreeType<ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c))
                 ]
                ];
            public static IEnumerable<object[]> FourTypeData => [
                [1, 120, new FourType<ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableFourType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                (a, b, c, d) => new ImmutableFourType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d))
             ]
                ];
            public static IEnumerable<object[]> FiveTypeData => [
                [1, 110, new FiveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableFiveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                (a, b, c, d, e) => new ImmutableFiveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e))
             ]
            ];
            public static IEnumerable<object[]> SixTypeData => [
                [1, 110, new SixType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableSixType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                (a, b, c, d, e, f) => new ImmutableSixType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f))
             ]
            ];
            public static IEnumerable<object[]> SevenTypeData => [
                [1, 110, new SevenType<ImmutableType,
                                       ImmutableType,
                                       ImmutableType,
                                       ImmutableType,
                                       ImmutableType,
                                       ImmutableType,
                                       ImmutableType,
                                       ImmutableSevenType<
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType,
                                           ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                (a, b, c, d, e, f, g) => new ImmutableSevenType<
                    ImmutableType,
                    ImmutableType,
                    ImmutableType,
                    ImmutableType,
                    ImmutableType,
                    ImmutableType,
                    ImmutableType,
                    ImmutableType>(a, b, c, d, e, f, g))]];
            public static IEnumerable<object[]> EightTypeData => [
                [1, 110, new EightType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableEightType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                (a, b, c, d, e, f, g, h) => new ImmutableEightType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h))
             ],
            ];
            public static IEnumerable<object[]> NineTypeData => [
                [1, 110, new NineType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableNineType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                (a, b, c, d, e, f, g, h, i) => new ImmutableNineType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i))
             ],
            ];
            public static IEnumerable<object[]> TenTypeData => [
                [1, 110, new TenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableTenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                (a, b, c, d, e, f, g, h, i, j) => new ImmutableTenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j))
             ],
            ];
            public static IEnumerable<object[]> ElevenTypeData => [
                [1, 110, new ElevenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableElevenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                (a, b, c, d, e, f, g, h, i, j, k) => new ImmutableElevenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k))
             ],
            ];
            public static IEnumerable<object[]> TwelveTypeData => [
                [1, 110, new TwelveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableTwelveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                    Build(x=>x.WithId(12)),
                (a, b, c, d, e, f, g, h, i, j, k, l) => new ImmutableTwelveType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k, l))
             ],
            ];
            public static IEnumerable<object[]> ThirteenTypeData => [
                [1, 110, new ThirteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableThirteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                    Build(x=>x.WithId(12)),
                    Build(x=>x.WithId(13)),
                (a, b, c, d, e, f, g, h, i, j, k, l, m) => new ImmutableThirteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k, l, m))
             ],
            ];
            public static IEnumerable<object[]> FourteenTypeData => [
                [1, 110, new FourteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableFourteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                    Build(x=>x.WithId(12)),
                    Build(x=>x.WithId(13)),
                    Build(x=>x.WithId(14)),
                (a, b, c, d, e, f, g, h, i, j, k, l, m, n) => new ImmutableFourteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k, l, m, n))
             ],
            ];
            public static IEnumerable<object[]> FifteenTypeData => [
                [1, 110, new FifteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableFifteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                    Build(x=>x.WithId(12)),
                    Build(x=>x.WithId(13)),
                    Build(x=>x.WithId(14)),
                    Build(x=>x.WithId(15)),
                (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) => new ImmutableFifteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o))
             ],
            ];
            public static IEnumerable<object[]> SixteenTypeData => [
                [1, 110, new SixteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType,
                ImmutableSixteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>>(
                    Build(x=>x.WithId()),
                    Build(x=>x.WithId(2)),
                    Build(x=>x.WithId(3)),
                    Build(x=>x.WithId(4)),
                    Build(x=>x.WithId(5)),
                    Build(x=>x.WithId(6)),
                    Build(x=>x.WithId(7)),
                    Build(x=>x.WithId(8)),
                    Build(x=>x.WithId(9)),
                    Build(x=>x.WithId(10)),
                    Build(x=>x.WithId(11)),
                    Build(x=>x.WithId(12)),
                    Build(x=>x.WithId(13)),
                    Build(x=>x.WithId(14)),
                    Build(x=>x.WithId(15)),
                    Build(x=>x.WithId(16)),
                (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) => new ImmutableSixteenType<ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType, ImmutableType>(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p))
             ],
            ];

        }

        public class Multiple
        {
            public static IEnumerable<object[]> SingleTypeData => [
                [1, new SingleType<ImmutableType>(new ImmutableType(1, "entry 1", 10, DateTime.Today))],
                [1, new SingleType<MutableType>(new MutableType { Id = 1, Name = "entry 1", Value = 10, Modified = DateTime.Today })],
                [2, new SingleType<RecordType>(new RecordType(2, "entry 2", 20, DateTime.Today))],
                [3, new SingleType<PrimaryConstructorImmutableType>(new PrimaryConstructorImmutableType(3, "entry 3", 30, DateTime.Today))]
               ];

            public static IEnumerable<object[]> OneType => [
                [new OneType<
                        IEnumerable<ImmutableType>,
                        IEnumerable<ImmutableOneType<
                            IEnumerable<ImmutableType>,
                            IEnumerable<ImmutableType>>>>(
                    [
                        new ImmutableType(1, "entry 1", 10, DateTime.UtcNow.Date)
                    ],
                    (a) =>
                        [
                            new ImmutableOneType<IEnumerable<ImmutableType>, IEnumerable<ImmutableType>>(a)
                        ]
                        )
                    ]
          ];

            public static IEnumerable<object[]> TwoType => [
                [new TwoType<
                        IEnumerable<ImmutableType>,
                        IEnumerable<ImmutableType>,
                        IEnumerable<ImmutableTwoType<
                            IEnumerable<ImmutableType>,
                            IEnumerable<ImmutableType>,
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    (a,b) =>
                        [
                            new ImmutableTwoType<IEnumerable<ImmutableType>, IEnumerable<ImmutableType>, IEnumerable<ImmutableType>>(a, b)
                        ]
                        )
                    ]
                ];

            public static IEnumerable<object[]> ThreeType => [
                [new ThreeType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableThreeType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    (a,b, c) =>
                        [
                            new ImmutableThreeType<
                                IEnumerable<ImmutableType>, // 1
                                IEnumerable<ImmutableType>, // 2
                                IEnumerable<ImmutableType>, // 3
                                IEnumerable<ImmutableType>>(a, b, c)
                        ]
                        )
                    ]
                ];

            public static IEnumerable<object[]> FourType => [
                [new FourType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableFourType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    (a, b, c, d) =>
                        [
                            new ImmutableFourType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>>(a, b, c, d)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> FiveType => [
                [new FiveType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableFiveType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    (a, b, c, d, e) =>
                        [
                            new ImmutableFiveType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>>(a, b, c, d, e)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> SixType => [
                [new SixType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6
                        IEnumerable<ImmutableSixType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    (a, b, c, d, e, f) =>
                        [
                            new ImmutableSixType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> SevenType => [
                [new SevenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableSevenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    (a, b, c, d, e, f, g) =>
                        [
                            new ImmutableSevenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> EightType => [
                [new EightType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableEightType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    (a, b, c, d, e, f, g, h) =>
                        [
                            new ImmutableEightType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> NineType => [
                [new NineType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableNineType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    (a, b, c, d, e, f, g, h, i) =>
                        [
                            new ImmutableNineType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> TenType => [
                [new TenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableTenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    (a, b, c, d, e, f, g, h, i, j) =>
                        [
                            new ImmutableTenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j)

                        ])
                    ]
                ];
            public static IEnumerable<object[]> ElevenType => [
                [new ElevenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableElevenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    (a, b, c, d, e, f, g, h, i, j, k) =>
                        [
                            new ImmutableElevenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k)

                        ]
                        )
                    ]
                ];
            public static IEnumerable<object[]> TwelveType => [
                [new TwelveType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableType>, // 12
                        IEnumerable<ImmutableTwelveType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>, // 12
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    Build(12),
                    (a, b, c, d, e, f, g, h, i, j, k, l) =>
                        [
                            new ImmutableTwelveType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>, // 12
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k, l)

                        ]
                        )
                    ]
                ];
            public static IEnumerable<object[]> ThirteenType => [
                [new ThirteenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableType>, // 12
                        IEnumerable<ImmutableType>, // 13
                        IEnumerable<ImmutableThirteenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>, // 12
                            IEnumerable<ImmutableType>, // 13
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    Build(12),
                    Build(13),
                    (a, b, c, d, e, f, g, h, i, j, k, l, m) =>
                        [
                            new ImmutableThirteenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>, // 12
                                    IEnumerable<ImmutableType>, // 13
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k, l, m)

                        ]
                        )
                    ]
                ];
            public static IEnumerable<object[]> FourteenType => [
                [new FourteenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableType>, // 12
                        IEnumerable<ImmutableType>, // 13
                        IEnumerable<ImmutableType>, // 14 
                        IEnumerable<ImmutableFourteenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>, // 12
                            IEnumerable<ImmutableType>, // 13
                            IEnumerable<ImmutableType>, // 14
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    Build(12),
                    Build(13),
                    Build(14),
                    (a, b, c, d, e, f, g, h, i, j, k, l, m, n) =>
                        [
                            new ImmutableFourteenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>, // 12
                                    IEnumerable<ImmutableType>, // 13
                                    IEnumerable<ImmutableType>, // 14
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k, l, m, n)

                        ]
                        )
                    ]
                ];
            public static IEnumerable<object[]> FifteenType => [
                [new FifteenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableType>, // 12
                        IEnumerable<ImmutableType>, // 13
                        IEnumerable<ImmutableType>, // 14
                        IEnumerable<ImmutableType>, // 15 
                        IEnumerable<ImmutableFifteenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>, // 12
                            IEnumerable<ImmutableType>, // 13
                            IEnumerable<ImmutableType>, // 14
                            IEnumerable<ImmutableType>, // 15
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    Build(12),
                    Build(13),
                    Build(14),
                    Build(15),
                    (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o) =>
                        [
                            new ImmutableFifteenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>, // 12
                                    IEnumerable<ImmutableType>, // 13
                                    IEnumerable<ImmutableType>, // 14
                                    IEnumerable<ImmutableType>, // 15
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o)

                        ]
                        )
                    ]
                ];
            public static IEnumerable<object[]> SixteenType => [
                [new SixteenType<
                        IEnumerable<ImmutableType>, // 1
                        IEnumerable<ImmutableType>, // 2
                        IEnumerable<ImmutableType>, // 3
                        IEnumerable<ImmutableType>, // 4
                        IEnumerable<ImmutableType>, // 5
                        IEnumerable<ImmutableType>, // 6 
                        IEnumerable<ImmutableType>, // 7
                        IEnumerable<ImmutableType>, // 8
                        IEnumerable<ImmutableType>, // 9 
                        IEnumerable<ImmutableType>, // 10
                        IEnumerable<ImmutableType>, // 11
                        IEnumerable<ImmutableType>, // 12
                        IEnumerable<ImmutableType>, // 13
                        IEnumerable<ImmutableType>, // 14
                        IEnumerable<ImmutableType>, // 15 
                        IEnumerable<ImmutableType>, //16
                        IEnumerable<ImmutableSixteenType<
                            IEnumerable<ImmutableType>, // 1
                            IEnumerable<ImmutableType>, // 2
                            IEnumerable<ImmutableType>, // 3
                            IEnumerable<ImmutableType>, // 4
                            IEnumerable<ImmutableType>, // 5
                            IEnumerable<ImmutableType>, // 6 
                            IEnumerable<ImmutableType>, // 7
                            IEnumerable<ImmutableType>, // 8
                            IEnumerable<ImmutableType>, // 9 
                            IEnumerable<ImmutableType>, // 10
                            IEnumerable<ImmutableType>, // 11
                            IEnumerable<ImmutableType>, // 12
                            IEnumerable<ImmutableType>, // 13
                            IEnumerable<ImmutableType>, // 14
                            IEnumerable<ImmutableType>, // 15
                            IEnumerable<ImmutableType>, // 16
                            IEnumerable<ImmutableType>>>>(
                    Build(),
                    Build(2),
                    Build(3),
                    Build(4),
                    Build(5),
                    Build(6),
                    Build(7),
                    Build(8),
                    Build(9),
                    Build(10),
                    Build(11),
                    Build(12),
                    Build(13),
                    Build(14),
                    Build(15),
                    Build(16),
                    (a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p) =>
                        [
                            new ImmutableSixteenType<
                                    IEnumerable<ImmutableType>, // 1
                                    IEnumerable<ImmutableType>, // 2
                                    IEnumerable<ImmutableType>, // 3
                                    IEnumerable<ImmutableType>, // 4
                                    IEnumerable<ImmutableType>, // 5
                                    IEnumerable<ImmutableType>, // 6 
                                    IEnumerable<ImmutableType>, // 7
                                    IEnumerable<ImmutableType>, // 8
                                    IEnumerable<ImmutableType>, // 9 
                                    IEnumerable<ImmutableType>, // 10
                                    IEnumerable<ImmutableType>, // 11
                                    IEnumerable<ImmutableType>, // 12
                                    IEnumerable<ImmutableType>, // 13
                                    IEnumerable<ImmutableType>, // 14
                                    IEnumerable<ImmutableType>, // 15
                                    IEnumerable<ImmutableType>, // 16
                                    IEnumerable<ImmutableType>>(a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p)

                        ]
                        )
                    ]
                ];
        }

        public class ImmutableTypeOptionsBuilder
        {
            public static ImmutableType Build(Action<ImmutableTypeOptions> builder)
            {
                var options = new ImmutableTypeOptions();
                builder(options);
                return options.Build();
            }

            public static IEnumerable<ImmutableType> Build(int instances = 1) => Enumerable.Range(1, instances).Select(x => Build(y => y.WithId(x)));


        }

        public class ImmutableTypeOptions
        {
            private int _id;
            private string _name;
            private decimal _value = 1;
            private DateTime _modified = DateTime.UtcNow.Date;

            public ImmutableTypeOptions()
            {
            }

            public ImmutableTypeOptions WithId(int id = 1)
            {
                _id = id;
                return this;
            }

            public ImmutableTypeOptions WithName(string name = "entry")
            {
                _name = name;
                return this;
            }

            public ImmutableTypeOptions WithValue(decimal value = 10)
            {
                _value = value;
                return this;
            }

            public ImmutableTypeOptions WithDate(DateTime? modified = null)
            {
                _modified = modified ?? DateTime.Today;
                return this;
            }

            protected internal ImmutableType Build()
            {
                return new ImmutableType(
                    _id,
                    _name ?? $"entry {_id}",
                    _value == 0 ? _value : (_id * 10),
                    _modified
                    );
            }
        }
    }
}

