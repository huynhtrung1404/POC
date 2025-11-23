import type { FC } from "react";
import { Navigate } from "react-router-dom";

interface ProtectedRoute {
  children: React.ReactNode;
}

const ProtectedRoute: FC<ProtectedRoute> = ({ children }) => {
  const isAuthenticated = !!localStorage.getItem("token");
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }
  return children;
};

export default ProtectedRoute;
