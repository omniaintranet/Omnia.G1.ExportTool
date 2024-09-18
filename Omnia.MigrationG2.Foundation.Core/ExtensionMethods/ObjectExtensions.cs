using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns true if the object is null, if a string is null, if a guid is empty
        /// </summary>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static bool IsNull<T>(this T theObject)
        {

            object obj = theObject;

            if (theObject is string)
                return string.IsNullOrWhiteSpace((string)obj) ? true : false;
            if (theObject is Guid && theObject != null)
                return ((Guid)obj).Equals(Guid.Empty);
            return theObject == null ? true : false;
        }

        /// <summary>
        /// Invokes action if the object is null, if a string is null, if a guid is empty
        /// </summary>
        /// <param name="theObject"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static T IsNull<T>(this T theObject, Action action)
        {
            if (IsNull(theObject))
            {
                action.Invoke();
            }
            return theObject;
        }

        /// <summary>
        /// Returns true if the object is not null, if a string is not null, if a guid is not empty
        /// </summary>
        /// <param name="theObject"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>(this T theObject)
        {
            return !IsNull(theObject);
        }

        /// <summary>
        /// Invokes action if the object is not null, if a string is not null, if a guid is not empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theObject">The object.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static T IsNotNull<T>(this T theObject, Action action)
        {
            if (!IsNull(theObject))
            {
                action.Invoke();
            }
            return theObject;
        }

        /// <summary>
        /// Determines whether the object is null and throws generic Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theObject">The object.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static T IsNullThrow<T>(this T theObject, string message)
        {
            theObject.IsNull(() => {

                throw new Exception(message);
            });

            return theObject;
        }

        /// <summary>
        /// Determines whether the object is null and throws provided Exception
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="theObject">The object.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static T IsNullThrow<T>(this T theObject, Exception exception)
        {
            theObject.IsNull(() =>
            {
                throw exception;
            });

            return theObject;
        }

        /// <summary>
        /// Determines whether:
        /// string is the same using StringComparison.OrdinalIgnoreCase
        /// guid is the same
        /// if the object is the same
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <param name="objectToCompare">The object to compare.</param>
        /// <returns></returns>
        public static bool Is(this object theObject, Object objectToCompare)
        {
            if (theObject.IsNull())
                return false;
            if (theObject is int && objectToCompare is int)
                return ((int)theObject).Equals((int)objectToCompare);
            if (theObject is bool && objectToCompare is bool)
                return ((bool)theObject).Equals((bool)objectToCompare);
            if (theObject is string && objectToCompare is string)
                return ((string)theObject).Equals((string)objectToCompare, StringComparison.OrdinalIgnoreCase);
            if (theObject is Guid && theObject != null)
                return ((Guid)theObject).Equals((Guid)objectToCompare);
            if (theObject is Enum)
                return ((Enum)theObject).Equals((Enum)objectToCompare);

            throw new NotSupportedException("This object type is not handled by Is or IsNot extension method");

        }

        /// <summary>
        /// Invokes action if: 
        /// string is the same using StringComparison.OrdinalIgnoreCase
        /// guid is the same 
        /// or if the object is the same
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <param name="objectToCompare">The object to compare.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static object Is(this object theObject, Object objectToCompare, Action action)
        {
            if (Is(theObject, objectToCompare))
            {
                action.Invoke();
            }
            return theObject;
        }

        /// <summary>
        /// Determines whether:
        /// string is not the same using StringComparison.OrdinalIgnoreCase
        /// guid is not the same 
        /// or if the object is not the same
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <param name="objectToCompare">The object to compare.</param>
        /// <returns></returns>
        public static bool IsNot(this object theObject, Object objectToCompare)
        {
            return !Is(theObject, objectToCompare);
        }

        /// <summary>
        /// Invokes action if not: 
        /// string is the same using StringComparison.OrdinalIgnoreCase
        /// guid is the same 
        /// or if the object is the same
        /// </summary>
        /// <param name="theObject">The object.</param>
        /// <param name="objectToCompare">The object to compare.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public static object IsNot(this object theObject, Object objectToCompare, Action action)
        {
            if (!Is(theObject, objectToCompare))
            {
                action.Invoke();
            }
            return theObject;
        }
    }
}
