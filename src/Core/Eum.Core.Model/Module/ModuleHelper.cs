using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eum.Core.Module
{
    public static class ModuleHelper
    {
        public static IEnumerable<IEumModule> GetModuleImplementations(this IEnumerable<Assembly> assemblies)
        {
            var list = new List<IEumModule>();
            foreach (var assembly in assemblies)
            {
                list.AddRange(GetImplementations<IEumModule>(assembly));
            }
            return list;
        }

        public static IEnumerable<IEumModule> GetModuleImplementations(this Assembly assembly)
        {
            return GetImplementations<IEumModule>(assembly);
        }

        public static IEnumerable<T> GetImplementations<T>(Assembly assembly)
        {
            var types = GetImplementTypes<T>(assembly);
            // 인스턴스 생성
            return types
                .Where(t => t.IsClass && t.IsPublic && !t.IsAbstract)
                .Select(t => (T)Activator.CreateInstance(t));
        }

        public static IEnumerable<Type> GetImplementationTypes<T>(Assembly assembly)
        {
            var types = GetImplementTypes<T>(assembly);
            // 인스턴스 생성
            return types
                .Where(t => t.IsClass && t.IsPublic && !t.IsAbstract);
        }

        static IEnumerable<Type> GetImplementTypes<T>(Assembly assembly)
        {
            // 노출된 형식들 중
            var exportedTypes = assembly.ExportedTypes;
            // T를 상속받아 구현한 형식만 조회
            var moduleTypes = exportedTypes.Where(IsImplemented<T>);
            return moduleTypes;
        }

        /// <summary>
        /// Determines whether this instance is implemented.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The t.</param>
        /// <returns>
        ///   <c>true</c> if the specified t is implemented; otherwise, <c>false</c>.
        /// </returns>
        static bool IsImplemented<T>(this Type t)
        {
            return t.GetTypeInfo()
                // 타입의 부모 인터페이스
                .ImplementedInterfaces
                // T와 일치하는 형식이 있는지 조회
                .Where(it => it == typeof(T))
                // T를 구현하고 있을 경우
                .Count() > 0;
        }

        /// <summary>
        /// RCL 의 wwwroot 폴더에 포함 된 파일은  _content/{LIBRARY NAME}/ 와 같은 규칙으로 노출된다. 
        /// 예를 들어, 이름이 Razor.Class.Lib 인 라이브러리는 에서 정적 컨텐츠에 대한 경로를 생성 _content/Razor.Class.Lib/합니다.
        /// https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-3.0&tabs=visual-studio#consume-content-from-a-referenced-rcl
        /// </summary>
        /// <returns></returns>
        public static string GetRclStaticContentPath(string alter)
        {
            var moduleName = Assembly.GetCallingAssembly().GetName().Name.Replace(".Views", "");
            if (alter.StartsWith("/")) alter = alter.Remove(0, 1);
            if (moduleName.StartsWith("Eum.Module"))
            {
                return $"~/_content/{moduleName}/{alter}";
            }
            else
            {
                return $"~/{alter}";
            }
        }

        public static string GetRootUrl(string path)
        {
            var configuration = Static.Resolve<IConfiguration>();
            var root = configuration.GetValue("AppSettings:rootPath", "");
            return root + path;
        }
    }
}
