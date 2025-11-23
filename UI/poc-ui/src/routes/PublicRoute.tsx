import type { FC } from "react";
import { Navigate } from "react-router-dom";

interface PublicRouteProp {
  children: React.ReactNode;
}

const PublicRoute: FC<PublicRouteProp> = ({ children }) => {
  const isAuthenticated = !!localStorage.getItem("token");
  if (isAuthenticated) {
    return <Navigate to="/dashboard" replace />;
  }
  return children;
};

export default PublicRoute;
