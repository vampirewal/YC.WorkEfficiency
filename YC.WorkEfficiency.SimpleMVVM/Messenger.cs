﻿/* 项目名称： Messenger.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: Messenger
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:31:26
 * 更新时间 : 2021/2/20 18:31:26
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 消息通知类
    /// </summary>
    public class Messenger
    {
        private Messenger()
        {

        }

        /// <summary>
        /// 注册消息
        /// </summary>
        public void RegistAll()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IMessageManager))))
                            .ToArray();
            foreach (var v in types)
            {
                object obj = Activator.CreateInstance(v);
                MethodInfo[] methods = obj.GetType().GetMethods();
                foreach (var item in methods)
                {
                    RegistMethodAttribute attribute = item.GetCustomAttribute<RegistMethodAttribute>();
                    if (attribute != null)
                    {
                        if (attribute.Token == null)
                        {
                            Messenger.Default.Register(obj, item.Name, item);
                        }
                        else
                        {
                            Messenger.Default.Register(obj, attribute.Token, item);
                        }
                    }
                }
            }
        }

        public void RegistObject(object obj)
        {
            MethodInfo[] methods = obj.GetType().GetMethods();
            foreach (var item in methods)
            {
                RegistMethodAttribute attribute = item.GetCustomAttribute<RegistMethodAttribute>();
                if (attribute != null)
                {
                    if (attribute.Token == null)
                    {
                        Messenger.Default.Register(obj, item.Name, item);
                    }
                    else
                    {
                        Messenger.Default.Register(obj, attribute.Token, item);
                    }
                }
            }
        }
        private static Messenger instance;

        /// <summary>
        /// 默认单例实例
        /// </summary>
        public static Messenger Default
        {
            get
            {
                if (instance == null)
                {
                    instance = new Messenger();
                }

                return instance;
            }
        }

        Dictionary<string, TokenInstance> tokenAndInstance = new Dictionary<string, TokenInstance>();

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        /// <exception cref="MessageRegisteredException"></exception>
        public void Register(object register, string token, Action action)
        {
            TokenInstance tokenInstance = new TokenInstance();
            tokenInstance.Register = register;
            tokenInstance.MethodInfo = action.Method;
            try
            {
                tokenAndInstance.Add(token, tokenInstance);
            }
            catch (Exception)
            {

                throw new MessageRegisteredException("该Token消息已注册");
            }

        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <param name="methodInfo"></param>
        /// <exception cref="MessageRegisteredException"></exception>
        public void Register(object register, string token, MethodInfo methodInfo)
        {
            TokenInstance tokenInstance = new TokenInstance();
            tokenInstance.Register = register;
            tokenInstance.MethodInfo = methodInfo;
            try
            {
                tokenAndInstance.Add(token, tokenInstance);
            }
            catch (Exception)
            {

                throw new MessageRegisteredException("该Token消息已注册");
            }

        }

        /// <summary>
        /// 注册消息
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        /// <exception cref="MessageRegisteredException"></exception>
        public void Register<T>(object register, string token, Action<T> action)
        {
            TokenInstance tokenInstance = new TokenInstance();
            tokenInstance.Register = register;
            tokenInstance.MethodInfo = action.Method;
            try
            {
                tokenAndInstance.Add(token, tokenInstance);
            }
            catch (Exception)
            {

                throw new MessageRegisteredException("该Token消息已注册");
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TReturn">返回值类型</typeparam>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        public void Register<T, TReturn>(object register, string token, Func<T, TReturn> action)
        {
            TokenInstance tokenInstance = new TokenInstance();
            tokenInstance.Register = register;
            tokenInstance.MethodInfo = action.Method;
            try
            {
                tokenAndInstance.Add(token, tokenInstance);
            }
            catch (Exception)
            {

                throw new MessageRegisteredException("该Token消息已注册");
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="TReturn">返回值类型</typeparam>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <param name="action"></param>
        public void Register<TReturn>(object register, string token, Func<TReturn> action)
        {
            TokenInstance tokenInstance = new TokenInstance();
            tokenInstance.Register = register;
            tokenInstance.MethodInfo = action.Method;
            try
            {
                tokenAndInstance.Add(token, tokenInstance);
            }
            catch (Exception)
            {

                throw new MessageRegisteredException("该Token消息已注册");
            }
        }

        /// <summary>
        /// 卸载消息
        /// </summary>
        /// <param name="register"></param>
        public void Unregister(object register)
        {
            List<string> key = new List<string>();

            foreach (var item in tokenAndInstance.Keys)
            {
                if (register == tokenAndInstance[item].Register)
                {
                    key.Add(item);
                }
            }

            foreach (var item in key)
            {
                tokenAndInstance.Remove(item);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parameters"></param>
        /// <exception cref="MessageNotFoundException"></exception>
        public void Send(string token, params object[] parameters)
        {
            try
            {
                tokenAndInstance[token].MethodInfo.Invoke(tokenAndInstance[token].Register, parameters);
            }
            catch (KeyNotFoundException)
            {
                throw new MessageNotFoundException("未找到该消息");
            }

        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="token"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="MessageNotFoundException"></exception>
        public T Send<T>(string token, params object[] parameters)
        {
            try
            {
                return (T)tokenAndInstance[token].MethodInfo.Invoke(tokenAndInstance[token].Register, parameters);
            }
            catch (KeyNotFoundException)
            {
                throw new MessageNotFoundException("未找到该消息");
            }
        }
    }
}
