// Copyright (c) 2007, Clarius Consulting, Manas Technology Solutions, InSTEDD, and Contributors.
// All rights reserved. Licensed under the BSD 3-Clause License; see License.txt.

using System;
using System.ComponentModel;
using System.Linq.Expressions;

using Moq.Language;
using Moq.Language.Flow;

namespace Moq.Protected
{
	/// <summary>
	/// Allows setups to be specified for protected members (methods and properties)
	/// seen through another type with corresponding members (that is, members
	/// having identical signatures as the ones to be set up).
	/// </summary>
	/// <typeparam name="T">Type of the mocked object.</typeparam>
	/// <typeparam name="TAnalog">
	/// Any type with members whose signatures are identical to the mock's protected members (except for their accessibility level).
	/// </typeparam>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IProtectedAsMock<T, TAnalog> : IFluentInterface
		where T : class
		where TAnalog : class
	{
		/// <summary>
		/// Accesses the underlying Mock<typeparamref name="T"/>.
		/// </summary>
		Mock<T> UnderlyingMock { get; }

		/// <summary>
		/// Specifies a setup on the mocked type for a call to a <see langword="void"/> method.
		/// </summary>
		/// <param name="expression">Lambda expression that specifies the expected method invocation.</param>
		/// <seealso cref="Mock{T}.Setup(Expression{Action{T}})"/>
		ISetup<T> Setup(Expression<Action<TAnalog>> expression);

		/// <summary>
		/// Specifies a setup on the mocked type for a call to a value-returning method.
		/// </summary>
		/// <typeparam name="TResult">Type of the return value. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the expected method invocation.</param>
		/// <seealso cref="Mock{T}.Setup{TResult}(Expression{Func{T, TResult}})"/>
		ISetup<T, TResult> Setup<TResult>(Expression<Func<TAnalog, TResult>> expression);

		/// <summary>
		/// Specifies a setup on the mocked type for a call to a property getter.
		/// </summary>
		/// <typeparam name="TProperty">Type of the property. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the property getter.</param>
		ISetupGetter<T, TProperty> SetupGet<TProperty>(Expression<Func<TAnalog, TProperty>> expression);

		/// <summary>
		/// Specifies that the given property should have "property behavior",
		/// meaning that setting its value will cause it to be saved and later returned when the property is requested.
		/// (This is also known as "stubbing".)
		/// </summary>
		/// <typeparam name="TProperty">Type of the property. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the property.</param>
		/// <param name="initialValue">Initial value for the property.</param>
		Mock<T> SetupProperty<TProperty>(Expression<Func<TAnalog, TProperty>> expression, TProperty initialValue = default(TProperty));

		/// <summary>
		/// Return a sequence of values, once per call.
		/// </summary>
		/// <typeparam name="TResult">Type of the return value. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the expected method invocation.</param>
		ISetupSequentialResult<TResult> SetupSequence<TResult>(Expression<Func<TAnalog, TResult>> expression);

		/// <summary>
		/// Performs a sequence of actions, one per call.
		/// </summary>
		/// <param name="expression">Lambda expression that specifies the expected method invocation.</param>
		ISetupSequentialAction SetupSequence(Expression<Action<TAnalog>> expression);

		/// <summary>
		/// Verifies that a specific invocation matching the given expression was performed on the mock.
		/// Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
		/// </summary>
		/// <param name="expression">Lambda expression that specifies the method invocation.</param>
		/// <param name="times">
		/// Number of times that the invocation is expected to have occurred.
		/// If omitted, assumed to be <see cref="Times.AtLeastOnce"/>.
		/// </param>
		/// <param name="failMessage">Message to include in the thrown <see cref="MockException"/> if verification fails.</param>
		/// <exception cref="MockException">The specified invocation did not occur (or did not occur the specified number of times).</exception>
		void Verify(Expression<Action<TAnalog>> expression, Times? times = null, string failMessage = null);

		/// <summary>
		/// Verifies that a specific invocation matching the given expression was performed on the mock.
		/// Use in conjunction with the default <see cref="MockBehavior.Loose"/>.
		/// </summary>
		/// <typeparam name="TResult">Type of the return value. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the method invocation.</param>
		/// <param name="times">
		/// Number of times that the invocation is expected to have occurred.
		/// If omitted, assumed to be <see cref="Times.AtLeastOnce"/>.
		/// </param>
		/// <param name="failMessage">Message to include in the thrown <see cref="MockException"/> if verification fails.</param>
		/// <exception cref="MockException">The specified invocation did not occur (or did not occur the specified number of times).</exception>
		void Verify<TResult>(Expression<Func<TAnalog, TResult>> expression, Times? times = null, string failMessage = null);

		/// <summary>
		/// Verifies that a property was read on the mock.
		/// </summary>
		/// <typeparam name="TProperty">Type of the property. Typically omitted as it can be inferred from the expression.</typeparam>
		/// <param name="expression">Lambda expression that specifies the method invocation.</param>
		/// <param name="times">
		/// Number of times that the invocation is expected to have occurred.
		/// If omitted, assumed to be <see cref="Times.AtLeastOnce"/>.
		/// </param>
		/// <param name="failMessage">Message to include in the thrown <see cref="MockException"/> if verification fails.</param>
		/// <exception cref="MockException">The specified invocation did not occur (or did not occur the specified number of times).</exception>
		void VerifyGet<TProperty>(Expression<Func<TAnalog, TProperty>> expression, Times? times = null, string failMessage = null);
	}
}
