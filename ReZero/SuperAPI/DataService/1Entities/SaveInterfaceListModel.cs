﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListModel
    {
        public long Id { get; set; }
        public string? Url { get; set; }
        public string? GroupName { get; set; }
        public string? TableId { get; set; }
        public string? Name { get; set; }
        public string? InterfaceCategoryId { get; set; }
        public ActionType? ActionType { get; set; }
        public CommonQueryConfig? Json { get; set; }
        public bool PageSize { get; set; }
    }


    public class CommonQueryConfig
    {
        public CommonQueryColumn[]? Columns { get; set; }
        public CommonQueryComplexitycolumn[]? ComplexityColumns { get; set; }
        public CommonQueryWhere[]? Where { get; set; }
        public WhereRelation? WhereRelation { get; set; }
        public string? WhereRelationTemplate { get; set; }
        public CommonQueryOrderby[]? OrderBys { get; set; }
        public bool OrderBysEnableSort { get; set; }
    }

    public class CommonQueryColumn
    {
        public string? Id { get; set; } 
        public string? DbColumnName { get; set; }
        public string? PropertyName { get; set; } 
        public int SortId { get; set; } 
    }

    public class CommonQueryComplexitycolumn
    {
        public string? PropertyName { get; set; }
        public string? DbColumnName { get; set; }
        public int SortId { get; set; }
        public CommonQueryComplexitycolumnJson? Json { get; set; }
    }
    public class CommonQueryComplexitycolumnJson 
    {
        public CommonQueryComplexitycolumnJoinInfo? JoinInfo { get; set; }
    }
    public class CommonQueryComplexitycolumnJoinInfo
    {
        public string? MasterField { get; set; }
        public string? JoinTable { get; set; }
        public ColumnJoinType? JoinType { get; set; }
        public string? JoinField { get; set; }
        public string? ShowField { get; set; }
        public string? Name { get; set; }
        public int? SortId { get; set; }
    }

    public class CommonQueryWhere
    {
        public int Id { get; set; }
        public string? PropertyName { get; set; }
        public string? WhereType { get; set; }
        public WhereValueType ValueType { get; set; }
        public string? Value { get; set; }  
    }

    public class CommonQueryOrderby
    {
        public string? Name { get; set; }
        public string? OrderByType { get; set; }
        public string? SortId { get; set; }
    }

}
