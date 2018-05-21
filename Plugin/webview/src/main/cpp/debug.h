#pragma once
#include <string>

class debug
{
public:
  using log_func = void(*)(const char* message);
  debug(log_func func);
  void log(const std::string& message);

private:
  log_func func;
};
