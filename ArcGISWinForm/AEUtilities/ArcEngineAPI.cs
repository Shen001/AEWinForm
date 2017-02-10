using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;

namespace SeanShen.AEUtilities
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/02/10,周五 10:03:57
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  8bfd1f45-5053-4fa5-a0ce-9c411ad46b6d
    ** 描述： 该文件包括与AE相关的一些操作类
    *******************************/
    public class ArcEngineAPI_Geometry
    {
        /// <summary>
        /// 获得一个Geometry的所有顶点
        /// </summary>
        /// <param name="sourceGeom"></param>
        /// <returns></returns>
        public static IMultipoint GetVertices(IGeometry pGeometry)
        {
            if (pGeometry == null) return null;

            IPointCollection pPointCollection = new MultipointClass();

            object obj = null;
            if (pGeometry is IPoint)
            {
                pPointCollection.AddPoint(pGeometry as IPoint, ref obj, ref obj);
                return pPointCollection as IMultipoint;
            }
            else if (pGeometry is ISegment)
            {
                ISegment pSegment = pGeometry as ISegment;
                pPointCollection.AddPoint(pSegment.FromPoint, ref obj, ref obj);
                pPointCollection.AddPoint(pSegment.ToPoint, ref obj, ref obj);
            }
            else if (pGeometry is IEnvelope)
            {
                IEnvelope pEnvelope = pGeometry as IEnvelope;
                pPointCollection.AddPoint(pEnvelope.UpperLeft, ref obj, ref obj);
                pPointCollection.AddPoint(pEnvelope.UpperRight, ref obj, ref obj);
                pPointCollection.AddPoint(pEnvelope.LowerLeft, ref obj, ref obj);
                pPointCollection.AddPoint(pEnvelope.LowerRight, ref obj, ref obj);
            }
            else if (pGeometry is IGeometryCollection)
            {
                IGeometryCollection pGeometryCollection = pGeometry as IGeometryCollection;
                for (int i = 0; i < pGeometryCollection.GeometryCount; i++)
                {
                    IGeometry pSubGeo = pGeometryCollection.get_Geometry(i);
                    IPointCollection pSubPointCollection = GetVertices(pSubGeo) as IPointCollection;
                    if (pSubPointCollection != null)
                    {
                        pPointCollection.AddPointCollection(pSubPointCollection);
                    }
                }
            }

            if (pPointCollection.PointCount == 0)
                return null;
            else
                return pPointCollection as IMultipoint;
        }

        /// <summary>
        /// 把geometry合并起来,组成一个新的geometry,在缓冲buffer个单位
        /// </summary>
        /// <param name="lstGeo">Geo列表</param>
        /// <param name="buffer">缓冲单位</param>
        /// <returns></returns>
        public static IGeometry GetUnionGeometry(IList<IGeometry> lstGeo, double buffer)
        {
            if (lstGeo.Count == 0) return null;
            IGeometryBag pGeometryBag = new GeometryBagClass();
            pGeometryBag.SpatialReference = lstGeo[0].SpatialReference;
            IGeometryCollection pGeometryCollection = pGeometryBag as IGeometryCollection;
            object obj = Type.Missing;
            foreach (IGeometry geo in lstGeo)
            {
                pGeometryCollection.AddGeometry(geo, ref obj, ref obj);
            }

            ITopologicalOperator UnionPolygon = new PolygonClass();
            UnionPolygon.ConstructUnion(pGeometryCollection as ESRI.ArcGIS.Geometry.IEnumGeometry);
            IGeometry geoResult = UnionPolygon as IGeometry;
            ITopologicalOperator operatorTopo = geoResult as ITopologicalOperator;

            return operatorTopo.Buffer(buffer);
        }

    }
    public class ArcEngineAPI_Common
    {
        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pSrc"></param>
        /// <returns></returns>
        public static IClone CreateClone<T>(T pSrc) where T : class,IClone
        {
            IClone pClone = pSrc as IClone;
            if (pClone == null) return default(T);//使用 default 关键字，此关键字对于引用类型会返回 null，对于数值类型会返回零。 对于结构，此关键字将返回初始化为零或 null 的每个结构成员，具体取决于这些结构是值类型还是引用类型。 对于可以为 null 的值类型，默认返回 System.Nullable<T>，它像任何结构一样初始化。
            return pClone.Clone();
        }
    }
    public class AarcEngineAPI_Symbol
    {
        /// <summary>
        /// 点样式，没有设置outline
        /// </summary>
        /// <param name="color"></param>
        /// <param name="size"></param>
        /// <param name="estyle"></param>
        /// <returns></returns>
        public static IMarkerSymbol CreateMarkerSymbol(IRgbColor color, double size, esriSimpleMarkerStyle estyle)
        {
            ISimpleMarkerSymbol pSymbol = new SimpleMarkerSymbolClass();
            pSymbol.Color = color as IColor;
            pSymbol.Size = size;
            pSymbol.Style = estyle;

            return pSymbol;
        }

        public static ILineSymbol CreateLineSymbol(IRgbColor color, double width, esriSimpleLineStyle estyle)
        {
            ISimpleLineSymbol pSymbol = new SimpleLineSymbolClass();
            pSymbol.Color = color as IColor;
            pSymbol.Width = width;
            pSymbol.Style = estyle;

            return pSymbol;
        }

        public static IFillSymbol CreateFillSymbol(IRgbColor color, esriSimpleFillStyle estyle)
        {
            ISimpleFillSymbol pSymbol = new SimpleFillSymbolClass();
            pSymbol.Color = color as IColor;
            pSymbol.Style = estyle;
            //outline的样式可以从线样式获取
            return pSymbol;
        }
    }
}
