using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Ptv.Controls.Map
{
    public class InteractiveRenderer
    {
        public static Region DrawLineString(System.Drawing.Graphics g, SharpMap.Geometries.LineString line, System.Drawing.Pen pen, SharpMap.Map map)
        {
            if (line.Vertices.Count > 1)
            {
                System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
                PointF[] points = line.TransformToImage(map);
                gp.AddLines(points);
                g.DrawPath(pen, gp);

                GisSharpBlog.NetTopologySuite.Geometries.Coordinate[] coords = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate[points.Length];
                for (int i = 0; i < points.Length; i++)
                    coords[i] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(points[i].X, points[i].Y);

                GisSharpBlog.NetTopologySuite.Geometries.LineString ls = new
                    GisSharpBlog.NetTopologySuite.Geometries.LineString(coords);
                GisSharpBlog.NetTopologySuite.Geometries.Coordinate[] linearRingPoints = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate[5];
                linearRingPoints[0] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(0, 0);
                linearRingPoints[1] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(map.Size.Width, 0);
                linearRingPoints[2] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(map.Size.Width, map.Size.Height);
                linearRingPoints[3] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(0, map.Size.Height);
                linearRingPoints[4] = new GisSharpBlog.NetTopologySuite.Geometries.Coordinate(0, 0);
                GisSharpBlog.NetTopologySuite.Geometries.LinearRing ring = new GisSharpBlog.NetTopologySuite.Geometries.LinearRing(linearRingPoints);
                GeoAPI.Geometries.IGeometry geom = ls.Intersection(new GisSharpBlog.NetTopologySuite.Geometries.Polygon(ring));

                System.Drawing.Drawing2D.GraphicsPath gp2 = new System.Drawing.Drawing2D.GraphicsPath();
                Pen p2 = new Pen(Color.Black, pen.Width < 4 ? 4 : pen.Width);

                if (geom is GisSharpBlog.NetTopologySuite.Geometries.LineString)
                {
                    GisSharpBlog.NetTopologySuite.Geometries.LineString lineString = geom as GisSharpBlog.NetTopologySuite.Geometries.LineString;
                    System.Drawing.PointF[] v = new System.Drawing.PointF[lineString.Coordinates.Length];
                    for (int i = 0; i < lineString.Coordinates.Length; i++)
                        v[i] = new PointF((float)lineString.Coordinates[i].X, (float)lineString.Coordinates[i].Y);

                    gp2.AddLines(v);
                }
                if (geom is GisSharpBlog.NetTopologySuite.Geometries.GeometryCollection)
                {
                    GisSharpBlog.NetTopologySuite.Geometries.GeometryCollection geomCollection = geom as GisSharpBlog.NetTopologySuite.Geometries.GeometryCollection;
                    foreach (GisSharpBlog.NetTopologySuite.Geometries.Geometry cGeom in geomCollection)
                    {
                        if (cGeom is GisSharpBlog.NetTopologySuite.Geometries.LineString)
                        {
                            GisSharpBlog.NetTopologySuite.Geometries.LineString lineString = cGeom as GisSharpBlog.NetTopologySuite.Geometries.LineString;
                            System.Drawing.PointF[] v = new System.Drawing.PointF[lineString.Coordinates.Length];
                            for (int i = 0; i < lineString.Coordinates.Length; i++)
                                v[i] = new PointF((float)lineString.Coordinates[i].X, (float)lineString.Coordinates[i].Y);

                            gp2.AddLines(v);
                        }
                    }
                }

                try
                {
                    gp2.Widen(p2);

                    System.Drawing.Region r = new System.Drawing.Region(gp2);
                    return r;
                }
                catch (OutOfMemoryException)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Renders a point to the map.
        /// </summary>
        /// <param name="g">Graphics reference</param>
        /// <param name="point">Point to render</param>
        /// <param name="symbol">Symbol to place over point</param>
        /// <param name="symbolscale">The amount that the symbol should be scaled. A scale of '1' equals to no scaling</param>
        /// <param name="offset">Symbol offset af scale=1</param>
        /// <param name="rotation">Symbol rotation in degrees</param>
        /// <param name="map">Map reference</param>
        public static Region DrawPoint(System.Drawing.Graphics g, SharpMap.Geometries.Point point, System.Drawing.Bitmap symbol, float symbolscale, System.Drawing.PointF offset, float rotation, SharpMap.Map map)
        {
            if (point == null)
                return null;
            if (symbol == null) //We have no point style - Use a default symbol
                symbol = SharpMap.Rendering.VectorRenderer.defaultsymbol;

            GraphicsPath gp = null;

            System.Drawing.PointF pp = SharpMap.Utilities.Transform.WorldtoMap(point, map);

            Matrix startingTransform = g.Transform;

            if (rotation != 0 && !Single.IsNaN(rotation))
            {
                System.Drawing.PointF rotationCenter = System.Drawing.PointF.Add(pp, new System.Drawing.SizeF(symbol.Width / 2, symbol.Height / 2));
                Matrix transform = new Matrix();
                transform.RotateAt(rotation, rotationCenter);

                g.Transform = transform;

                if (symbolscale == 1f)
                    g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2 + offset.X), (int)(pp.Y - symbol.Height / 2 + offset.Y));
                else
                {
                    float width = symbol.Width * symbolscale;
                    float height = symbol.Height * symbolscale;
                    g.DrawImage(symbol, (int)pp.X - width / 2 + offset.X * symbolscale, (int)pp.Y - height / 2 + offset.Y * symbolscale, width, height);
                }

                g.Transform = startingTransform;
            }
            else
            {
                if (symbolscale == 1f)
                {
                    g.DrawImageUnscaled(symbol, (int)(pp.X - symbol.Width / 2 + offset.X), (int)(pp.Y - symbol.Height / 2 + offset.Y));
                    gp = new GraphicsPath();
                    gp.AddRectangle(new System.Drawing.Rectangle((int)(pp.X - symbol.Width / 2 + offset.X), (int)(pp.Y - symbol.Height / 2 + offset.Y), symbol.Width, symbol.Height));
                }
                else
                {
                    float width = symbol.Width * symbolscale;
                    float height = symbol.Height * symbolscale;
                    g.DrawImage(symbol, (int)pp.X - width / 2 + offset.X * symbolscale, (int)pp.Y - height / 2 + offset.Y * symbolscale, width, height);
                }
            }

            if (gp != null)
                return new Region(gp);
            else
                return null;        }

        /// <summary>
        /// Renders a <see cref="SharpMap.Geometries.MultiPoint"/> to the map.
        /// </summary>
        /// <param name="g">Graphics reference</param>
        /// <param name="points">MultiPoint to render</param>
        /// <param name="symbol">Symbol to place over point</param>
        /// <param name="symbolscale">The amount that the symbol should be scaled. A scale of '1' equals to no scaling</param>
        /// <param name="offset">Symbol offset af scale=1</param>
        /// <param name="rotation">Symbol rotation in degrees</param>
        /// <param name="map">Map reference</param>
        public static void DrawMultiPoint(System.Drawing.Graphics g, SharpMap.Geometries.MultiPoint points, System.Drawing.Bitmap symbol, float symbolscale, System.Drawing.PointF offset, float rotation, SharpMap.Map map)
        {
            for (int i = 0; i < points.Points.Count; i++)
                DrawPoint(g, points.Points[i], symbol, symbolscale, offset, rotation, map);
        }
    }
}
