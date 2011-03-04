﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Libpc;
using System.Diagnostics;

namespace libpc_swig_test
{
    internal class TestLiblasReader : TestBase
    {
        public TestLiblasReader()
        {
            Test1();
        }

        private void Test1()
        {
            istream stream = Utils.openFile("../../test/data/1.2-with-color.las");

            LiblasReader reader = new LiblasReader(stream);

            ulong numPoints = reader.getNumPoints();

            Schema schema = reader.getHeader().getSchema();
            SchemaLayout layout = new SchemaLayout(schema);

            PointData data = new PointData(layout, 1);

            uint numRead = reader.read(data);

            {
                uint offsetX = (uint)schema.getDimensionIndex(Dimension.Field.Field_X);
                uint offsetY = (uint)schema.getDimensionIndex(Dimension.Field.Field_Y);
                uint offsetZ = (uint)schema.getDimensionIndex(Dimension.Field.Field_Z);

                uint index = 0;
                Int32 x0raw = data.getFieldInt32(index, offsetX);
                Int32 y0raw = data.getFieldInt32(index, offsetY);
                Int32 z0raw = data.getFieldInt32(index, offsetZ);
                double x0 = schema.getDimension(offsetX).getNumericValueInt32(x0raw);
                double y0 = schema.getDimension(offsetY).getNumericValueInt32(y0raw);
                double z0 = schema.getDimension(offsetZ).getNumericValueInt32(z0raw);

                Debug.Assert(x0 == 637012.240000);
                Debug.Assert(y0 == 849028.310000);
                Debug.Assert(z0 == 431.660000);
            }

            Utils.closeFile(stream);

            return;
        }
    }
}
