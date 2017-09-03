using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace GOB.SPF.ConecII.Entities.Attributes
{

    //string fieldName = Entity.GetField(x => x.Activo);
    //string nombre1 = Entity.GetMemberName(x => x.Activo);
    //var propertyInfo = StaticReflection.GetPropertyInfo(Entity, u => u.Activo);
    //string nombre2 = Entity.Activo.GetPropertyName(() => Entity.Activo);                
    public static class StaticReflection
    {
        public static List<string> GetRelatedField<T>(this T instance, Expression<Func<T, object>> expression)
        {
            string fielName = string.Empty;
            string property = string.Empty;
            List<string> propertField = new List<string>();
            
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));

            if (expression.Body.NodeType == System.Linq.Expressions.ExpressionType.Convert)
            {

                property = GetMemberName(expression);
                fielName = property + "Entity" + tableName.Value;
                propertField.Add(fielName);
            }
            else
            {
                NewExpression expressionNew = (NewExpression)expression.Body;
                foreach (Expression info in expressionNew.Arguments)
                {
                    property = GetMemberName(info);
                    fielName = property + "Entity" + tableName.Value;
                    propertField.Add(fielName);
                }
            }

            return propertField;
        }


        public static string GetAs<T>(this T instance, Expression<Func<T, object>> expression)
        {
            string fielNameAs = string.Empty;
            List<string> propertField = new List<string>();

            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            string property = GetMemberName(expression);



            if (expression.Body.NodeType == System.Linq.Expressions.ExpressionType.Convert)
            {
                property = GetMemberName(expression);
                fielNameAs =  property + " AS " + property + "Entity" + tableName.Value;
                propertField.Add(fielNameAs);
            }
            else
            {
                NewExpression expressionNew = (NewExpression)expression.Body;
                foreach (Expression info in expressionNew.Arguments)
                {
                    property = GetMemberName(info);
                    fielNameAs = property + " AS " + property + "Entity" + tableName.Value;
                    propertField.Add(fielNameAs);
                }
            }

            return string.Join(", ", propertField);
        }

        public static string GetAs<T>(this T instance, Expression<Func<T, object>> expression, string alias)
        {
            string fielNameAs = string.Empty;
            List<string> propertField = new List<string>();

            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            string property = GetMemberName(expression);



            if (expression.Body.NodeType == System.Linq.Expressions.ExpressionType.Convert)
            {
                property = GetMemberName(expression);
                fielNameAs = alias + "." + property + " AS " + property + "Entity" + tableName.Value;
                propertField.Add(fielNameAs);
            }
            else
            {
                NewExpression expressionNew = (NewExpression)expression.Body;
                foreach (Expression info in expressionNew.Arguments)
                {
                    property = GetMemberName(info);
                    fielNameAs = alias + "." + property + " AS " + property + "Entity" + tableName.Value;
                    propertField.Add(fielNameAs);
                }
            }

            return string.Join(", ", propertField);
        }

    

        public static string GetFieldAs<T>(this T instance, Expression<Func<T, object>> expression)
        {
            string fielName = string.Empty;
            string fielNameAs = string.Empty;
            string property;
            List<string> propertField = new List<string>();

            SchemaNameAttribute schemaName = (SchemaNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SchemaNameAttribute));
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
                        

            if (schemaName != null)
                fielName = schemaName.Value + ".";

            if (tableName != null)
                fielName = fielName + tableName.Value + ".";

            
            if (expression.Body.NodeType == System.Linq.Expressions.ExpressionType.Convert)
            {
                property = GetMemberName(expression);
                fielNameAs = fielName + property;
                fielNameAs = fielNameAs + " AS " + property + "Entity" + tableName.Value;
                propertField.Add(fielNameAs);
            }
            else
            {
                NewExpression expressionNew = (NewExpression)expression.Body;
                foreach (Expression info in expressionNew.Arguments)
                {
                    property = GetMemberName(info);
                    fielNameAs = fielName + property;
                    fielNameAs = fielNameAs + " AS " + property + "Entity" + tableName.Value;
                    propertField.Add(fielNameAs);
                }

            }
            
            return string.Join(", ", propertField);
        }
        
        public static string GetField<T>(this T instance, Expression<Func<T, object>> expression)
        {
            string fielName = string.Empty;
            SchemaNameAttribute schemaName = (SchemaNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SchemaNameAttribute));
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            string property = GetMemberName(expression);

            if (schemaName != null)            
                fielName = schemaName.Value + ".";

            if (tableName != null)
                fielName = fielName  + tableName.Value + ".";

            fielName = fielName + property;

            return fielName;
        }

        public static string GetFieldPrefix<T>(this T instance, Expression<Func<T, object>> expression)
        {
            string prefix = "P.";
            string property = GetMemberName(expression);

            
            prefix = prefix + property;

            return prefix;
        }

        public static string GetFieldPrefix<T>(this T instance, Expression<Func<T, object>> expression, string prefix)
        {

            string property = GetMemberName(expression);


            prefix = prefix + property;

            return prefix;
        }

        

        public static string GetTableFrom<T>(
            this T instance)
        {
            string tableFrom = string.Empty;
            SchemaNameAttribute schemaName = (SchemaNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(SchemaNameAttribute));
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            
            if (schemaName != null)
                tableFrom = schemaName.Value + ".";

            if (tableName != null)
                tableFrom = tableFrom + tableName.Value;


            return tableFrom;
        }

        public static string GetTableName<T>(
            this T instance)
        {
            
            TableNameAttribute tableName = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            return  tableName.Value;


            
        }

        public static string GetMemberName<T>(
            this T instance,
            Expression<Func<T, object>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(
            Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        public static string GetMemberName<T>(
            this T instance,
            Expression<Action<T>> expression)
        {
            return GetMemberName(expression);
        }

        public static string GetMemberName<T>(
            Expression<Action<T>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            return GetMemberName(expression.Body);
        }

        private static string GetMemberName(
            Expression expression)
        {
            if (expression == null)
            {
                throw new ArgumentException(
                    "The expression cannot be null.");
            }

            if (expression is MemberExpression)
            {
                // Reference type property or field
                var memberExpression =
                    (MemberExpression)expression;
                return memberExpression.Member.Name;
            }

            if (expression is MethodCallExpression)
            {
                // Reference type method
                var methodCallExpression =
                    (MethodCallExpression)expression;
                return methodCallExpression.Method.Name;
            }

            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                return GetMemberName(unaryExpression);
            }

            throw new ArgumentException("Invalid expression");
        }

        private static string GetMemberName(
            UnaryExpression unaryExpression)
        {
            if (unaryExpression.Operand is MethodCallExpression)
            {
                var methodExpression =
                    (MethodCallExpression)unaryExpression.Operand;
                return methodExpression.Method.Name;
            }

            return ((MemberExpression)unaryExpression.Operand)
                .Member.Name;
        }

        public static string GetPropertyName<T>(this T instance, Expression<Func<T>> expression)
        {
            MemberExpression body = (MemberExpression)expression.Body;
            return body.Member.Name;
        }

        public static PropertyInfo GetPropertyInfo<TSource, TProperty>(TSource source,Expression<Func<TSource, TProperty>> propertyLambda)
        {
            Type type = typeof(TSource);

            MemberExpression member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            PropertyInfo propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo;
        }
    }
}

///http://joelabrahamsson.com/getting-property-and-method-names-using-static-reflection-in-c/
//For the really interested person, it passes a number of tests, or executable specifications verified and generated using MSpec, producing the following specification:

//StaticReflection GetMemberName, given a Func returning a value type property 
//» should return the name of the property

//StaticReflection GetMemberName, given a Func returning a reference type property 
//» should return the name of the property

//StaticReflection GetMemberName, given a Func invoking a reference type method 
//» should return the name of the method

//StaticReflection GetMemberName, given a Func invoking a value type method 
//» should return the name of the method

//StaticReflection GetMemberName, given a Func invoking a void method 
//» should return the name of the method

//StaticReflection GetMemberName, given a Func invoking a method with a parameter 
//» should return the name of the method

//StaticReflection GetMemberName, called with null cast to Func 
//» should throw an exception 
//» should throw an ArgumentException

//StaticReflection GetMemberName, called with null cast to Action 
//» should throw an exception 
//» should throw an ArgumentException
