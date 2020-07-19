using MySql.Data.MySqlClient;
using NUnit.Framework;
using SqlBatis.Attributes;
using Dapper;
using SqlBatis.Expressions;
using SqlBatis.Test.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SqlBatis.Test
{
    public class UnitTests
    {

        [Test]
        public void TestInsertAsync()
        {
            var builder = new DbContextBuilder
            {
                Connection = new MySqlConnection("server=rm-bp16hgp1ext33r96b2o.mysql.rds.aliyuncs.com;user id=mammothcode;password=Jiuxian20180920;database=mammothcode_xiaoyema;"),
            };
            GlobalSettings.XmlCommandsProvider.Load(@"D:\SqlBatis\src\SqlBatis.Test\Student.xml");
            try
            {
                using (var db = new DbContext(builder))
                {
                    var ff = db.Execute("insert into advert_banners(banner_img,banner_sort,banner_group) values(@img,@sort,@group)", new { img=(string)null,sort=20, group=1 });
                }
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
    public class Student
    {
        public int Id { get; set; }
        [Column("is_del")]
        public byte[] IsDel { get; set; }
    }
    public class QueryProductGoodsListModel
    {
        /// <summary>
        /// Ʒ��id
        /// </summary>
        public int? BrandId { get; set; }
        /// <summary>
        /// ��Ӧ��id
        /// </summary>
        public int? SupplierId { get; set; }
        /// <summary>
        /// ��Ʒ���ࣺ1��
        /// </summary>
        public int[] CategoryId1 { get; set; }
        /// <summary>
        /// ��Ʒ���ࣺ2��
        /// </summary>
        public int[] CategoryId2 { get; set; }
        /// <summary>
        /// ��Ӧ�̵���id��һ��
        /// </summary>
        public int[] SupplierRegionId1 { get; set; }
        /// <summary>
        /// ��Ӧ�̵���id�ڶ���
        /// </summary>
        public int[] SupplierRegionId2 { get; set; }
        /// <summary>
        /// ��Ӧ����Ŀ
        /// </summary>
        public int? SupplierCategoryId { get; set; }
        /// <summary>
        /// �Ƿ��ؼ۴���
        /// </summary>
        public bool? IsPromoteSales { get; set; }
        /// <summary>
        /// �Ƿ���Ʒ�ϼ�
        /// </summary>
        public bool? IsNewArrivals { get; set; }
        /// <summary>
        /// �û�����
        /// </summary>
        public double? LocationLng { get; set; }
        /// <summary>
        /// �û�ά��
        /// </summary>
        public double? LocationLat { get; set; }
        /// <summary>
        /// 0����1����
        /// </summary>
        public int? SortType { get; set; }
        /// <summary>
        /// 0�������1�۸�
        /// </summary>
        public int? SortName { get; set; }
        /// <summary>
        /// ҳ��
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// ҳ��
        /// </summary>
        public int PageSize { get; set; }
    }
}