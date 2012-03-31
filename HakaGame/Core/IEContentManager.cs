using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    // Used for loading and unloading content
    public class IEContentManager : ContentManager
    {
        // Cache of all the loaded content
        Dictionary<string, object> loaded = new Dictionary<string, object>();
        List<IDisposable> disposableAssets = new List<IDisposable>();

        public IEContentManager(IServiceProvider services)
            : base(services)
        {
        }

        public IEContentManager(IServiceProvider services, string rootDirectory)
            : base(services, rootDirectory)
        {
        }

        // Load an asset and cache it
        public override T Load<T>(string assetName)
        {
            // Return the stored instance if there is one
            if (loaded.ContainsKey(assetName))
                return (T)loaded[assetName];

            // If there isn't, load a new one
            // Cant throw exception, would be nice if it could
            // Problems when cannot find asset
            T read = base.ReadAsset<T>(assetName, RecordDisposableAsset);

            loaded.Add(assetName, read);

            return read;
        }

        void RecordDisposableAsset(IDisposable disposable)
        {
            disposableAssets.Add(disposable);
        }

        // Load an asset and be guaranteed a clean copy of the object.
        // Note that if this function is used this copy of the asset cannot
        // be individually unloaded
        public T LoadFreshCopy<T>(string assetName)
        {
            return base.ReadAsset<T>(assetName, null);
        }

        // Unload a single asset
        public void UnloadAsset(string name)
        {
            if (loaded.ContainsKey(name))
            {
                if (loaded[name] is IDisposable && disposableAssets.Contains((IDisposable)loaded[name]))
                {
                    IDisposable disp = (IDisposable)loaded[name];
                    disposableAssets.Remove(disp);
                    disp.Dispose();
                }

                loaded.Remove(name);
            }
        }
    }
}
