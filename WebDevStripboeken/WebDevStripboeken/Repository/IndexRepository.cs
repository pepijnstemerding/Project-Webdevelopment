using System;
using Dapper;
using WebDevStripboeken.Models;

namespace WebDevStripboeken.Repository;

public class IndexRepository : DBConnection
{
    public int iets()
    {

        using var connection = Connect();
        
        return 6;
    }
}