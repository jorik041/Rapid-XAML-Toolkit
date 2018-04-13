﻿// <copyright file="GetCSharpPropertiesTests.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RapidXamlToolkit.Tests.Analysis
{
    [TestClass]
    public class GetCSharpPropertiesTests : CSharpTestsBase
    {
        [TestMethod]
        public void GetSimpleProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        public string Property1 { get; set; }

        *public string Property2 { get; set; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBox Text=\"{x:Bind Property2, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetSimpleReadOnlyProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        public string Property1 { get; set; }

        *public string Property2 { get; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBlock Text=\"Property2\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetSimpleReadOnlyPropertyPrivateSetter()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        public string Property1 { get; set; }

        *public string Property2 { get; private set; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBlock Text=\"Property2\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPropertyWithBackingField()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        private string _property2;

        public string Property1 { get; set; }

        *public string Property2 { get => _property2; set => _property2 = value; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBox Text=\"{x:Bind Property2, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPropertyWithBackingFieldAndPrivateSetter()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        private string _property2;

        public string Property1 { get; set; }

        *public string Property2 { get => _property2; private set => _property2 = value; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBlock Text=\"Property2\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPropertyWithBackingFieldAndNoSetter()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        private string _property2 = ""something"";

        public string Property1 { get; set; }

        *public string Property2 { get => _property2; }
*
        public string Property3 { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "Property2",
                Output = "<TextBlock Text=\"Property2\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetIntProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *public int SomeProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "SomeProperty",
                Output =
                    "<Slider Minimum=\"0\" Maximum=\"100\" x:Name=\"SomeProperty\" Value=\"{x:Bind SomeProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetGenericListProperty()
        {
            var code = @"
using System.Collections.Generic;

namespace tests
{
    class Class1
    {
        *public List<string> MyListProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "MyListProperty",
                Output = "<ItemsControl ItemsSource=\"{x:Bind MyListProperty}\"></ItemsControl>",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPrivateProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *private string TestProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "TestProperty",
                Output = "<TextBox Text=\"{x:Bind TestProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetProtectedProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *protected string TestProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "TestProperty",
                Output = "<TextBox Text=\"{x:Bind TestProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetInternalProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *internal string TestProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "TestProperty",
                Output = "<TextBox Text=\"{x:Bind TestProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPropertyWithoutModifier()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *string TestProperty { get; set; }*
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "TestProperty",
                Output = "<TextBox Text=\"{x:Bind TestProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetPropertyWithoutNamespace()
        {
            var code = @"
class Class1
{
    *string TestProperty { get; set; }*
}";

            var expected = new AnalyzerOutput
            {
                Name = "TestProperty",
                Output = "<TextBox Text=\"{x:Bind TestProperty, Mode=TwoWay}\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetCustomProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *public Order LastOrder { get; set; }*
    }

    class Order
    {
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = "<TextBlock Text=\"FALLBACK_LastOrder\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected);
        }

        [TestMethod]
        public void GetNativeProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *public Array LastOrder { get; set; }*
    }
}";

            var arrayProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FALLBACK_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "Array",
                        NameContains = "",
                        Output = "<TextBlock Text=\"{NAME}\" x:Array=\"true\" />",
                        IfReadOnly = false,
                    },
                },
            };

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = "<TextBlock Text=\"LastOrder\" x:Array=\"true\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected, arrayProfile);
        }

        [TestMethod]
        public void GetNonExistentPropertyProperty()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        *public InvalidType LastOrder { get; set; }*
    }
}";

            var invalidTypeProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FALLBACK_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "InvalidType",
                        NameContains = "",
                        Output = "<TextBlock Text=\"{NAME}\" x:InvalidType=\"true\" />",
                        IfReadOnly = false,
                    },
                },
            };

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = "<TextBlock Text=\"LastOrder\" x:InvalidType=\"true\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected, invalidTypeProfile);
        }

        [TestMethod]
        public void GetCustomPropertyWithSpecificMapping()
        {
            var orderProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FALLBACK_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "Order",
                        NameContains = "",
                        Output = "<TextBlock Text=\"{NAME}\" x:Order=\"true\" />",
                        IfReadOnly = false,
                    },
                },
            };

            var code = @"
namespace tests
{
    class Class1
    {
        *public Order LastOrder { get; set; }*
    }

    class Order
    {
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = "<TextBlock Text=\"LastOrder\" x:Order=\"true\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected, orderProfile);
        }

        [TestMethod]
        public void GetCustomPropertyAndSubProperties()
        {
            var recurseProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FB_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "Order",
                        NameContains = "",
                        Output = "<StackPanel>{SUBPROPERTIES}</StackPanel>",
                        IfReadOnly = false,
                    },
                },
            };

            var code = @"
namespace tests
{
    class Class1
    {
        *public Order LastOrder { get; set; }*
    }

    class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderPlacedDateTime { get; private set; }
        public string OrderDescription { get; set; }
    }
}";

            // This includes the readonly property as not yet filtering out
            // All types treated as fallback
            var expectedOutput = "<StackPanel>"
                                 + Environment.NewLine + "<TextBlock Text=\"FB_OrderId\" />"
                                 + Environment.NewLine + "<TextBlock Text=\"FB_OrderPlacedDateTime\" />"
                                 + Environment.NewLine + "<TextBlock Text=\"FB_OrderDescription\" />"
                                 + Environment.NewLine + "</StackPanel>";

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = expectedOutput,
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected, recurseProfile);
        }

        [TestMethod]
        public void HandleSubPropertiesOfSimpleProperty()
        {
            var recurseProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FB_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "string",
                        NameContains = "LastOrder",
                        Output = "<StackPanel>{SUBPROPERTIES}</StackPanel>",
                        IfReadOnly = false,
                    },
                },
            };

            var code = @"
namespace tests
{
    class Class1
    {
        *public string LastOrder { get; set; }*
    }
}";

            var expectedOutput = "<StackPanel>"
                                 + Environment.NewLine + "<TextBlock Text=\"FB_LastOrder\" />"
                                 + Environment.NewLine + "</StackPanel>";

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = expectedOutput,
                OutputType = AnalyzerOutputType.Property,
            };

            this.EachPositionBetweenStarsShouldProduceExpected(code, expected, recurseProfile);
        }

        [TestMethod]
        public void GetCustomProperty_InOtherFile()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        public Order LastOrd*er { get; set; }
    }
}";
            var code2 = @"
namespace tests
{
    class Order
    {
        public int OrderId { get; set; }
        public string OrderDescription { get; set; }
    }
}";

            var orderProfile = new Profile
            {
                Name = "GridTestProfile",
                ClassGrouping = "Grid",
                DefaultOutput = "<TextBlock Text=\"FB_{NAME}\" />",
                Mappings = new List<Mapping>
                {
                    new Mapping
                    {
                        Type = "Order",
                        NameContains = "",
                        Output = "<TextBlock x:IsOrder=\"True\" Text=\"{NAME}\" />",
                        IfReadOnly = false,
                    },
                },
            };

            var expected = new AnalyzerOutput
            {
                Name = "LastOrder",
                Output = "<TextBlock x:IsOrder=\"True\" Text=\"LastOrder\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.PositionAtStarShouldProduceExpectedUsingAdditonalFiles(code, expected, orderProfile, code2);
        }

        [TestMethod]
        public void GetCustomProperty_ForUndefinedType()
        {
            var code = @"
namespace tests
{
    class Class1
    {
        publ*ic NonDefinedType SomeProperty { get; set; }
    }
}";

            var expected = new AnalyzerOutput
            {
                Name = "SomeProperty",
                Output = "<TextBlock Text=\"FALLBACK_SomeProperty\" />",
                OutputType = AnalyzerOutputType.Property,
            };

            this.PositionAtStarShouldProduceExpected(code, expected);
        }
    }
}