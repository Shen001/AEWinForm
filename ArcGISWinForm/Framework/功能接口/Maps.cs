using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeanShen.Framework
{
    /*******************************
    ** 作者： shenxin
    ** 时间： 2017/01/20,周五 16:36:55
    ** 版本:  V1.0.0
    ** CLR:	  4.0.30319.18408	
    ** GUID:  ba396d00-15fa-4685-9d4c-2bb69c5b1a3e
    ** 描述： 实现IMaps接口为 IPageLayout.ReplaceMaps(IMaps maps)
    *******************************/

    using ESRI.ArcGIS.Carto;
    using System.Collections;
    public class Maps:IMaps,IDisposable
    {
        //class member - using internally an ArrayList to manage the Maps collection
        private ArrayList m_array = null;

        #region class constructor
        public Maps()
        {
            m_array = new ArrayList();
        }
        #endregion

        #region IDisposable Members

        /// <summary>
        /// Dispose the collection
        /// </summary>
        public void Dispose()
        {
            if (m_array != null)
            {
                m_array.Clear();
                m_array = null;
            }
        }

        #endregion

        #region IMaps Members

        /// <summary>
        /// Remove the Map at the given index
        /// </summary>
        /// <param name="Index"></param>
        public void RemoveAt(int Index)
        {
            if (Index > m_array.Count || Index < 0)
                throw new Exception("Maps::RemoveAt:\r\n索引超出范围!");

            m_array.RemoveAt(Index);
        }

        /// <summary>
        /// Reset the Maps array
        /// </summary>
        public void Reset()
        {
            m_array.Clear();
        }

        /// <summary>
        /// Get the number of Maps in the collection
        /// </summary>
        public int Count
        {
            get
            {
                return m_array.Count;
            }
        }

        /// <summary>
        /// Return the Map at the given index
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public IMap get_Item(int Index)
        {
            if (Index > m_array.Count || Index < 0)
                throw new Exception("Maps::get_Item:\r\n索引超出范围!");

            return m_array[Index] as IMap;
        }

        /// <summary>
        /// Remove the instance of the given Map
        /// </summary>
        /// <param name="Map"></param>
        public void Remove(IMap Map)
        {
            m_array.Remove(Map);
        }

        /// <summary>
        /// Create a new Map, add it to the collection and return it to the caller
        /// </summary>
        /// <returns></returns>
        public IMap Create()
        {
            IMap newMap = new MapClass();
            m_array.Add(newMap);

            return newMap;
        }

        /// <summary>
        /// Add the given Map to the collection
        /// </summary>
        /// <param name="Map"></param>
        public void Add(IMap Map)
        {
            if (Map == null)
                throw new Exception("Maps::Add:\r\nMap 为 null!");

            m_array.Add(Map);
        }

        #endregion
    }
}
