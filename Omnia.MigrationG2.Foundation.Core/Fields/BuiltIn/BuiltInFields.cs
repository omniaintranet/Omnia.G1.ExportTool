using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Fields.BuiltIn
{
    [DateTimeField(id: "{8c06beca-0777-48f7-91c7-6da68bc07b69}",
        internalName: "Created",
        Group = "_Hidden",
        ReadOnlyField = true,
        Title = "$Resources:core,Created;")]
    public class Created : FieldBase
    {
    }

    [UserField(id: "{1df5e554-ec7e-46a6-901d-d85a3881cb18}",
        internalName: "Author",
        Group = "_Hidden",
        ReadOnlyField = true,
        Title = "$Resources:core,Created_By;")]
    public class Author : FieldBase
    {
    }

    [UserField(id: "{d31655d1-1d5b-4511-95a1-7a09e9b75bf2}",
        internalName: "Editor",
        Group = "_Hidden",
        ReadOnlyField = true,
        Title = "$Resources:core,Modified_By;")]
    public class Editor : FieldBase
    {
    }

    [TextField(id: "{fa564e0f-0c70-4ab9-b863-0177e6ddd247}",
        internalName: "Title",
        Group = "_Hidden",
        Title = "$Resources:core,Title;",
        Required = true)]
    public class Title : FieldBase
    {
    }

    [DateTimeField(id: "{28cf69c5-fa48-462a-b5cd-27b6f9d2bd5f}",
        internalName: "Modified",
        Group = "_Hidden",
        ReadOnlyField = true,
        Title = "$Resources:core,Modified;")]
    public class Modified : FieldBase
    {
    }

    // Note that the type 'File' is not supported. Fallback to general FieldAttribute.
    [Field(id: "{8553196d-ec8d-4564-9861-3dbe931050c8}",
        internalName: "FileLeafRef",
        typeAsString: "File",
        Group = "_Hidden",
        ShowInVersionHistory = false,
        Title = "$Resources:core,Name;",
        Required = true)]
    public class FileLeafRef : FieldBase
    {
    }

    [ManagedMetadataField(id: "{23f27201-bee3-471e-b2e7-b64fd8b7ca38}",
        internalName: "TaxKeyword", isMulti: false,
        Group = "Enterprise Keywords Group")]
    public class EnterpriseKeyword : FieldBase
    {
    }
}
