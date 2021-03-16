using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  //generic abstract class
  public abstract class Base<T>
  {
    #region Properties
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Date Created
    /// </summary>
    public DateTime DateCreated { get; set; }
    /// <summary>
    /// date updated
    /// </summary>
    public DateTime? DateUpdated { get; set; }
    #endregion
    /// <summary>
    /// add record
    /// </summary>
    /// <returns></returns>
    public abstract Task addRecord();
    /// <summary>
    /// update record
    /// </summary>
    /// <returns></returns>
    public abstract Task updateRecord();
    /// <summary>
    /// get record
    /// </summary>
    /// <returns></returns>
    public abstract Task<List<T>> getRecords();

    /// <summary>
    /// 
    /// Get category
    /// </summary>
    /// <returns></returns>
    public abstract Task<T> getRecord();


  }
}
