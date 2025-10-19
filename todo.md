# Syrx TODO List

This document tracks outstanding tasks, improvements, and issues that need to be addressed in the Syrx framework.

## Documentation Issues

### High Priority

- [ ] **Fix "Configuration from External Sources" section in configuration-guide.md**
  - **Issue**: The current implementation shown in the configuration guide is incorrect
  - **Details**: The example shows a generic approach that doesn't align with the actual external configuration implementations that exist outside this solution
  - **Action Required**: Update the section to reflect the correct patterns used in the actual external configuration providers
  - **Location**: `.docs/configuration-guide.md` - line 381 and following section
  - **Priority**: High - This could mislead developers trying to implement external configuration

- [ ] **Restore accurate "Provider Implementation Pattern" section in architecture.md**
  - **Issue**: The provider implementation pattern examples were inconsistent with real-world implementations
  - **Details**: The code examples showed generic patterns that don't match the actual provider implementation structure used in practice
  - **Action Required**: Research actual provider implementations and add accurate examples that reflect real-world usage
  - **Location**: `.docs/architecture.md` - Database Provider Architecture section
  - **Priority**: High - Developers need accurate patterns for creating custom providers

- [ ] **Add correct "Multiple Result Sets" documentation to icommander-guide.md**
  - **Issue**: The Multiple Result Sets section was incorrect and misleading
  - **Details**: Multiple result set support should be handled by Query overloads, not the Execute methods with Func<TResult> parameters
  - **Action Required**: Document the correct approach using Query overloads for handling multiple result sets from stored procedures
  - **Location**: `.docs/icommander-guide.md` - was removed from section between Multi-map Queries and Method Resolution
  - **Priority**: High - Important feature that needs accurate documentation

### Medium Priority

- [ ] **Review and validate all code examples in documentation**
  - Ensure all code examples compile and work with the current API
  - Verify that all method signatures match the actual implementation

- [ ] **Add performance benchmarks documentation**
  - Document performance characteristics compared to raw Dapper
  - Include guidance on optimization strategies

### Low Priority

- [ ] **Expand testing documentation**
  - Create comprehensive testing guide
  - Include examples for mocking and integration tests

## Framework Enhancements

### Planned Features

- [ ] **Enhanced logging and diagnostics**
  - Add structured logging for command resolution
  - Include performance metrics and timing information

- [ ] **Configuration validation tools**
  - Runtime configuration validation
  - Startup validation helpers

- [ ] **Additional database provider support**
  - Investigate support for newer database providers
  - Document provider development guidelines

### Bug Fixes

- [ ] **Review multi-mapping performance**
  - Investigate potential optimizations for complex multi-mapping scenarios
  - Consider caching strategies for mapping functions

## Code Quality

### Technical Debt

- [ ] **Standardize error messages**
  - Ensure consistent error messaging across all components
  - Add error codes for better categorization

- [ ] **Improve test coverage**
  - Identify areas with insufficient test coverage
  - Add integration tests for all database providers

### Refactoring

- [ ] **Review naming conventions**
  - Ensure all public APIs follow consistent naming patterns
  - Update any inconsistent method or property names

## External Dependencies

### Provider Implementations

- [ ] **Update external configuration provider examples**
  - Work with external configuration provider teams to ensure documentation accuracy
  - Get real-world examples that can be included in documentation

- [ ] **Coordinate with database provider packages**
  - Ensure all database provider packages are up to date
  - Verify compatibility with latest database driver versions

## Release Planning

### Version 3.0.0

- [ ] **Complete migration to .NET 8.0**
  - Ensure all packages target .NET 8.0
  - Update all dependencies to compatible versions

- [ ] **Breaking changes documentation**
  - Document any breaking changes from previous versions
  - Provide migration guides where necessary

### Future Versions

- [ ] **Consider .NET 9.0 support**
  - Evaluate new features that could benefit Syrx
  - Plan migration timeline

## Contributions Needed

### Community

- [ ] **Seek community feedback on API design**
  - Gather input on current API usability
  - Identify pain points in current implementation

- [ ] **Encourage provider development**
  - Create clear guidelines for community-contributed providers
  - Establish testing and quality standards

---

## How to Contribute

1. **Pick an item** from this TODO list
2. **Create an issue** on GitHub referencing the TODO item
3. **Fork the repository** and create a feature branch
4. **Implement the changes** following the contribution guidelines
5. **Submit a pull request** with clear description of changes

## Priority Legend

- **High Priority**: Issues that affect functionality or could mislead users
- **Medium Priority**: Improvements that enhance user experience
- **Low Priority**: Nice-to-have features or minor improvements

---

*Last Updated: October 18, 2025*
