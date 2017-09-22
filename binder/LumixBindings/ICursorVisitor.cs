using System;
using ClangSharp;

namespace LumixBindings
{
    interface ICursorVisitor
    {
        CXChildVisitResult Visit(CXCursor cursor, CXCursor parent, IntPtr client_data);
    }
}
