using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data.OleDb;
using SharpMap.Data.Providers;
using SharpMap.Data;
using System.Data;

namespace Ptv.Controls.Map.MapMarket
{
    /// <summary>
    /// The MMProviderStatic provider is used for rendering geometry data from an unencrypted Map&Market7 / GIS-Layer
    /// compatible datasource. Unlike the normal MMProvider it loads all data into a feature data table
    /// and deserialies the geometry at the beginning. Therefore it's faster than the standard map&market
    /// provider, but needs more memory and doesn't reflect changes on the database.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public class MMProviderStatic : IProvider, IDisposable
    {
        private FeatureDataTable m_fdt;

        /// <summary>
        /// Initializes a new instance of the MMProvider
        /// </summary>
        /// <param name="ConnectionStr"></param>
        /// <param name="tablename"></param>
        /// <param name="OID_ColumnName"></param>
        /// <param name="xColumn"></param>
        /// <param name="yColumn"></param>
        public MMProviderStatic(string ConnectionStr, string tablename, string OID_ColumnName, string xMinColumnName, string yMinColumnName, string xMaxColumnName, string yMaxColumnName, string geometryColumnName)
        {
            this.Table = tablename;
            this.XMinColumn = xMinColumnName;
            this.YMinColumn = yMinColumnName;
            this.XMaxColumn = xMaxColumnName;
            this.YMaxColumn = yMaxColumnName;
            this.GeometryColumn = geometryColumnName;
            this.ObjectIdColumn = OID_ColumnName;
            this.ConnectionString = ConnectionStr;

            Reload();
        }

        /// <summary>
        /// reloads the data
        /// </summary>
        public void Reload()
        {
            using (System.Data.OleDb.OleDbConnection conn = new OleDbConnection(_ConnectionString))
            {
                string strSQL = "Select * FROM " + this.Table;

                using (System.Data.OleDb.OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, conn))
                {
                    conn.Open();
                    System.Data.DataSet ds2 = new System.Data.DataSet();
                    adapter.Fill(ds2);
                    conn.Close();
                    if (ds2.Tables.Count > 0)
                    {
                        m_fdt = new FeatureDataTable(ds2.Tables[0]);
                        foreach (System.Data.DataColumn col in ds2.Tables[0].Columns)
                            m_fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);
                        foreach (System.Data.DataRow dr in ds2.Tables[0].Rows)
                        {
                            SharpMap.Data.FeatureDataRow fdr = m_fdt.NewRow();
                            foreach (System.Data.DataColumn col in ds2.Tables[0].Columns)
                                fdr[col.ColumnName] = dr[col];
                            SharpMap.Geometries.Geometry geom = SharpMap.Converters.WellKnownBinary.GeometryFromWKB.Parse((byte[])dr[this.GeometryColumn]);
                            fdr.Geometry = geom;
                            m_fdt.AddRow(fdr);
                        }
                    }
                }
            }
        }

        private string _Table;

        /// <summary>
        /// Data table name
        /// </summary>
        public string Table
        {
            get { return _Table; }
            set { _Table = value; }
        }


        private string _ObjectIdColumn;

        /// <summary>
        /// Name of column that contains the Object ID
        /// </summary>
        public string ObjectIdColumn
        {
            get { return _ObjectIdColumn; }
            set { _ObjectIdColumn = value; }
        }

        private string _XColumn;

        /// <summary>
        /// Name of column that contains X coordinate
        /// </summary>
        public string XColumn
        {
            get { return _XColumn; }
            set { _XColumn = value; }
        }

        private string _XMinColumn;

        /// <summary>
        /// Name of column that contains XMin coordinate
        /// </summary>
        public string XMinColumn
        {
            get { return _XMinColumn; }
            set { _XMinColumn = value; }
        }

        private string _YMinColumn;

        /// <summary>
        /// Name of column that contains YMin coordinate
        /// </summary>
        public string YMinColumn
        {
            get { return _YMinColumn; }
            set { _YMinColumn = value; }
        }
        
        private string _XMaxColumn;

        /// <summary>
        /// Name of column that contains XMax coordinate
        /// </summary>
        public string XMaxColumn
        {
            get { return _XMaxColumn; }
            set { _XMaxColumn = value; }
        }
        
        private string _YMaxColumn;

        /// <summary>
        /// Name of column that contains YMax coordinate
        /// </summary>
        public string YMaxColumn
        {
            get { return _YMaxColumn; }
            set { _YMaxColumn = value; }
        }
        
        private string _GeometryColumn;

        /// <summary>
        /// Name of column that contains Geometry
        /// </summary>
        public string GeometryColumn
        {
            get { return _GeometryColumn; }
            set { _GeometryColumn = value; }
        }
        
        private string _YColumn;

        /// <summary>
        /// Name of column that contains Y coordinate
        /// </summary>
        public string YColumn
        {
            get { return _YColumn; }
            set { _YColumn = value; }
        }

        private string _ConnectionString;
        /// <summary>
        /// Connectionstring
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        #region IProvider Members

        /// <summary>
        /// Returns geometries within the specified bounding box
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        public Collection<SharpMap.Geometries.Geometry> GetGeometriesInView(SharpMap.Geometries.BoundingBox bbox)
        {
            string cond = string.Empty;
            if (!String.IsNullOrEmpty(_defintionQuery))
                cond += "(" + _defintionQuery + ") AND ";
            //Limit to the points within the boundingbox
            cond +=
                this.XMinColumn + " < " + System.Convert.ToInt32(bbox.Right).ToString() + " AND " +
                this.XMaxColumn + " > " + System.Convert.ToInt32(bbox.Left).ToString() + " AND " +
                this.YMinColumn + " < " + System.Convert.ToInt32(bbox.Top).ToString() + " AND " +
                this.YMaxColumn + " > " + System.Convert.ToInt32(bbox.Bottom).ToString();

            System.Data.DataView dv = new System.Data.DataView(m_fdt, cond, "", System.Data.DataViewRowState.CurrentRows);

            FeatureDataTable fdt = new FeatureDataTable(m_fdt);
            foreach (System.Data.DataColumn col in m_fdt.Columns)
                fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);

            Collection<SharpMap.Geometries.Geometry> result = new Collection<SharpMap.Geometries.Geometry>();

            for (int i = 0; i < dv.Count; i++)
            {
                SharpMap.Data.FeatureDataRow fdrSource = dv[i].Row as SharpMap.Data.FeatureDataRow;
                result.Add(fdrSource.Geometry);
            }

            return result;
        }

        /// <summary>
        /// Returns geometry Object IDs whose bounding box intersects 'bbox'
        /// </summary>
        /// <param name="bbox"></param>
        /// <returns></returns>
        public Collection<uint> GetObjectIDsInView(SharpMap.Geometries.BoundingBox bbox)
        {
            throw new NotSupportedException("GetObjectIDsInView(SharpMap.Geometries.BoundingBox bbox) is not supported by the MMProviderStatic.");
        }

        /// <summary>
        /// Returns the geometry corresponding to the Object ID
        /// </summary>
        /// <param name="oid">Object ID</param>
        /// <returns>geometry</returns>
        public SharpMap.Geometries.Geometry GetGeometryByID(uint oid)
        {
            throw new NotSupportedException("GetGeometryByID(uint oid) is not supported by the MMProviderStatic.");
        }

        /// <summary>
        /// Throws NotSupportedException. 
        /// </summary>
        /// <param name="geom"></param>
        /// <param name="ds">FeatureDataSet to fill data into</param>
        public void ExecuteIntersectionQuery(SharpMap.Geometries.Geometry geom, FeatureDataSet ds)
        {
            throw new NotSupportedException("ExecuteIntersectionQuery(Geometry) is not supported by the MMProviderStatic.");
        }

        /// <summary>
        /// Returns all features with the view box
        /// </summary>
        /// <param name="bbox">view box</param>
        /// <param name="ds">FeatureDataSet to fill data into</param>
        public void ExecuteIntersectionQuery(SharpMap.Geometries.BoundingBox bbox, FeatureDataSet ds)
        {
            string cond = string.Empty;
            if (!String.IsNullOrEmpty(_defintionQuery))
                cond += "(" + _defintionQuery + ") AND ";
            //Limit to the points within the boundingbox
            cond +=
                this.XMinColumn + " < " + System.Convert.ToInt32(bbox.Right).ToString() + " AND " +
                this.XMaxColumn + " > " + System.Convert.ToInt32(bbox.Left).ToString() + " AND " +
                this.YMinColumn + " < " + System.Convert.ToInt32(bbox.Top).ToString() + " AND " +
                this.YMaxColumn + " > " + System.Convert.ToInt32(bbox.Bottom).ToString();

            System.Data.DataView dv = new System.Data.DataView(m_fdt, cond, "", System.Data.DataViewRowState.CurrentRows);
           
            FeatureDataTable fdt = new FeatureDataTable(m_fdt);
            foreach (System.Data.DataColumn col in m_fdt.Columns)
                fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);

            for (int i = 0; i < dv.Count; i++)
            {
                SharpMap.Data.FeatureDataRow fdrSource = dv[i].Row as SharpMap.Data.FeatureDataRow;
                SharpMap.Data.FeatureDataRow fdrDest = fdt.NewRow();
                foreach (System.Data.DataColumn col in m_fdt.Columns)
                    fdrDest[col.ColumnName] = fdrSource[col.ColumnName];
                fdrDest.Geometry = fdrSource.Geometry;
                fdt.AddRow(fdrDest);
            }
            ds.Tables.Add(fdt);
        }

        /// <summary>
        /// Returns the number of features in the dataset
        /// </summary>
        /// <returns>Total number of features</returns>
        public int GetFeatureCount()
        {
            if (!String.IsNullOrEmpty(_defintionQuery))
                return m_fdt.Count;
            else 
                return new System.Data.DataView(m_fdt, _defintionQuery, "", System.Data.DataViewRowState.CurrentRows).Count;
        }

        private string _defintionQuery;

        /// <summary>
        /// Definition query used for limiting dataset
        /// </summary>
        public string DefinitionQuery
        {
            get { return _defintionQuery; }
            set { _defintionQuery = value; }
        }

        /// <summary>
        /// Returns a datarow based on a RowID
        /// </summary>
        /// <param name="RowID"></param>
        /// <returns>datarow</returns>
        public FeatureDataRow GetFeature(uint RowID)
        {
            using (System.Data.OleDb.OleDbConnection conn = new OleDbConnection(_ConnectionString))
            {
                string strSQL = "select * from " + this.Table + " WHERE " + this.ObjectIdColumn + "=" + RowID.ToString();

                using (System.Data.OleDb.OleDbDataAdapter adapter = new OleDbDataAdapter(strSQL, conn))
                {
                    conn.Open();
                    System.Data.DataSet ds = new System.Data.DataSet();
                    adapter.Fill(ds);
                    conn.Close();
                    if (ds.Tables.Count > 0)
                    {
                        FeatureDataTable fdt = new FeatureDataTable(ds.Tables[0]);
                        foreach (System.Data.DataColumn col in ds.Tables[0].Columns)
                            fdt.Columns.Add(col.ColumnName, col.DataType, col.Expression);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            System.Data.DataRow dr = ds.Tables[0].Rows[0];
                            SharpMap.Data.FeatureDataRow fdr = fdt.NewRow();
                            foreach (System.Data.DataColumn col in ds.Tables[0].Columns)
                                fdr[col.ColumnName] = dr[col];
                            if (dr[this.XColumn] != DBNull.Value && dr[this.YColumn] != DBNull.Value)
                                fdr.Geometry = new SharpMap.Geometries.Point(System.Convert.ToDouble(dr[this.XColumn]), System.Convert.ToDouble(dr[this.YColumn]));
                            return fdr;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
            }
        }

        /// <summary>
        /// Boundingbox of dataset
        /// </summary>
        /// <returns>boundingbox</returns>
        public SharpMap.Geometries.BoundingBox GetExtents()
        {
            System.Data.DataView dv = new System.Data.DataView(m_fdt, _defintionQuery, "", System.Data.DataViewRowState.CurrentRows);

            if (dv.Count == 0)
                return null;

            double xmin = Double.MaxValue;
            double ymin = Double.MaxValue;
            double xmax = Double.MinValue;
            double ymax = Double.MinValue;

            foreach (DataRowView drv in dv)
            {
                double txmin = System.Convert.ToDouble(drv["XMIN"]);
                if (txmin < xmin) xmin = txmin;

                double tymin = System.Convert.ToDouble(drv["YMIN"]);
                if (tymin < ymin) ymin = tymin;

                double txmax = System.Convert.ToDouble(drv["XMAX"]);
                if (txmax > xmax) xmax = txmax;

                double tymax = System.Convert.ToDouble(drv["YMAX"]);
                if (tymax > ymax) ymax = tymax;
            }

            return new SharpMap.Geometries.BoundingBox(xmin, ymin, xmax, ymax);
        }

        /// <summary>
        /// Gets the connection ID of the datasource
        /// </summary>
        public string ConnectionID
        {
            get { return _ConnectionString; }
        }

        /// <summary>
        /// Opens the datasource
        /// </summary>
        public void Open()
        {
            //Don't really do anything. OleDb's ConnectionPooling takes over here
            _IsOpen = true;
        }
        /// <summary>
        /// Closes the datasource
        /// </summary>
        public void Close()
        {
            //Don't really do anything. OleDb's ConnectionPooling takes over here
            _IsOpen = false;
        }

        private bool _IsOpen;

        /// <summary>
        /// Returns true if the datasource is currently open
        /// </summary>
        public bool IsOpen
        {
            get { return _IsOpen; }
        }

        private int _SRID = -1;
        /// <summary>
        /// The spatial reference ID (CRS)
        /// </summary>
        public int SRID
        {
            get { return _SRID; }
            set { _SRID = value; }
        }

        #endregion

        #region Disposers and finalizers
        private bool disposed = false;

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        internal void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~MMProviderStatic()
        {
            Dispose();
        }
        #endregion
    }
}
