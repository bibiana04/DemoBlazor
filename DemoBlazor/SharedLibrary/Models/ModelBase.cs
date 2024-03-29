using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using DemoBlazor.Shared;

public class ModelBase{
    private Dictionary<String, Dictionary<String, String>> _errors =
    new Dictionary<string, Dictionary<string, string>>();

    private void AddError(String fieldName, String ruleName, String errorText)
{
    if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName,
    new Dictionary<string, string>()); }
    if (_errors[fieldName].ContainsKey(ruleName))
     { _errors[fieldName].Remove(ruleName); }
     _errors[fieldName].Add(ruleName, errorText);
     OnModelChanged();
}
private void RemoveError(String fieldName, String ruleName)
{
  if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName,
    new Dictionary<string, string>()); }
  if (_errors[fieldName].ContainsKey(ruleName))
     { _errors[fieldName].Remove(ruleName);
    OnModelChanged();
        }
    }
public void CheckRules(String fieldName)
{
  var propertyInfo = this.GetType().GetProperty(fieldName);
  var attrInfos = propertyInfo.GetCustomAttributes(true);
  foreach (var attrInfo in attrInfos)
  {
    if (attrInfo is IModelRule modelrule)
    {
      var value = propertyInfo.GetValue(this);
      var result = modelrule.Validate(fieldName, value);
      if (result.IsValid)
      {
        RemoveError(fieldName, attrInfo.GetType().Name);
      }
      else
      {
        AddError(fieldName, attrInfo.GetType().Name, result.Message);
      }
    }
  }
}
 
public bool CheckRules()
{
  foreach (var propInfo in this.GetType().GetProperties(
    System.Reflection.BindingFlags.Public |
      System.Reflection.BindingFlags.Instance))
    CheckRules(propInfo.Name);
 
  return HasErrors();
}
public String Errors(String fieldName)
{
  if (!_errors.ContainsKey(fieldName)) { _errors.Add(fieldName,
    new Dictionary<string, string>()); }
  System.Text.StringBuilder sb = new System.Text.StringBuilder();
  foreach (var value in _errors[fieldName].Values)
    sb.AppendLine(value);
 
  return sb.ToString();
}
public bool HasErrors()
{
  foreach (var key in _errors.Keys)
    if (_errors[key].Keys.Count > 0) { return true; }
  return false;
}
public String GetValue(String fieldName)
{
  var propertyInfo = this.GetType().GetProperty(fieldName);
  var value = propertyInfo.GetValue(this);
 
  if (value != null) { return value.ToString(); }
  return String.Empty;           
}
public void SetValue(String fieldName, object value)
{
  var propertyInfo = this.GetType().GetProperty(fieldName);
  propertyInfo.SetValue(this, value);
  CheckRules(fieldName);
}
public event EventHandler<EventArgs> ModelChanged;
 
protected void OnModelChanged()
{
  ModelChanged?.Invoke(this, new EventArgs());
}
public interface IModelRule
{
  ValidationResult Validate(String fieldName, object fieldValue);
}

}
