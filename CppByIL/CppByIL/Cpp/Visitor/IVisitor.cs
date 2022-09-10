using CppByIL.Cpp.Syntax.Expressions;
using CppByIL.Cpp.Syntax.IL;
using CppByIL.Cpp.Syntax.Precessor;
using CppByIL.Cpp.Syntax.Statements;
using CppByIL.Cpp.Syntax.Types;

namespace CppByIL.Cpp.Visitor
{

    public interface ISynctaxNodeVisitor
    {
        void VisitIncludePrecessor(Include node);
        void VisitPragmaPrecessor(Pragma node);
        void VisitNamespaceDeclaration(NamespaceDeclaration node);
        void VisitVariableExpression(LocalVariableExpression node);
        void VisitClassDeclaration(ClassDeclaration node);
        void VisitMethodDefinition(MethodDefinition node);
        void VisitMethodDeclaration(MethodDeclaration node);
        void VisitMethodParameter(MethodParameter node);
        void VisitTypeReference(TypeReference node);
        void VisitBlockStatement(BlockStatement node);
        void VisitILStatement(ILStatement node);
        void VisitILInstruction(ILInstruction node);
        void VisitAssignmentExpression(AssignmentExpression node);
    }

}
