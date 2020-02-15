using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// an object that needs to store its data in saveGame needs to implement this
/// </summary>
public interface IPersistable {

    void Restore(SaveData data);
    SaveData Save();
}
