using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{

    public interface ISynctaxNodeVisitor
    {
        void VisitIncludePrecessor(Include node);
        void VisitPragmaPrecessor(Pragma node);
        void VisitNamespaceDeclaration(NamespaceDeclaration node);
        void VisitClassDeclaration(ClassDeclaration classDeclaration);
        void VisitMethodDeclaration(MethodDeclaration methodDeclaration);
        void VisitMethodParameterDeclaration(MethodParameterDeclaration parameterDeclaration);
        void VisitTypeReference(TypeReference typeReference);
    }

}
