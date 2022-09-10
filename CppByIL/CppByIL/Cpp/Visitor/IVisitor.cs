using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{

    public interface ISynctaxNodeVisitor
    {
        void VisitIncludePrecessor(Include node);
        void VisitPragmaPrecessor(Pragma node);
        void VisitNamespaceDeclaration(NamespaceDeclaration node);
        void VisitClassDeclaration(ClassDeclaration node);
        void VisitMethodDefinition(MethodDefinition node);
        void VisitMethodDeclaration(MethodDeclaration node);
        void VisitMethodParameter(MethodParameter node);
        void VisitTypeReference(TypeReference node);
    }

}
